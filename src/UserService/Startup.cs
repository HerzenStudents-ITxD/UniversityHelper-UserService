using HerzenHelper.Core.RedisSupport.Extensions;
using FluentValidation;
using HealthChecks.UI.Client;
using HerzenHelper.Core.BrokerSupport.Broker.Consumer;
using HerzenHelper.Core.BrokerSupport.Configurations;
using HerzenHelper.Core.BrokerSupport.Extensions;
using HerzenHelper.Core.BrokerSupport.Helpers;
using HerzenHelper.Core.BrokerSupport.Middlewares.Token;
using HerzenHelper.Core.Configurations;
using HerzenHelper.Core.EFSupport.Extensions;
using HerzenHelper.Core.EFSupport.Helpers;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Middlewares.ApiInformation;
using HerzenHelper.Core.RedisSupport.Configurations;
using HerzenHelper.Core.RedisSupport.Constants;
using HerzenHelper.Core.RedisSupport.Helpers;
using HerzenHelper.UserService.Broker.Consumers;
using HerzenHelper.UserService.Data.Provider.MsSql.Ef;
using HerzenHelper.UserService.Models.Dto.Configurations;
using HerzenHelper.UserService.Models.Dto.Requests.User;
using HerzenHelper.UserService.Validation.User;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using HerzenHelper.UserService.Validation.User.Interfaces;

namespace HerzenHelper.UserService
{
  public class Startup : BaseApiInfo
  {
    public const string CorsPolicyName = "LtDoCorsPolicy";
    private string redisConnStr;

    private readonly BaseServiceInfoConfig _serviceInfoConfig;
    private readonly RabbitMqConfig _rabbitMqConfig;

    public IConfiguration Configuration { get; }

    #region private methods

    private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
    {
      var builder = new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider();

      return builder
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value
        .InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>()
        .First();
    }

    #endregion

    #region public methods

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      _serviceInfoConfig = Configuration
        .GetSection(BaseServiceInfoConfig.SectionName)
        .Get<BaseServiceInfoConfig>();

      _rabbitMqConfig = Configuration
        .GetSection(BaseRabbitMqConfig.SectionName)
        .Get<RabbitMqConfig>();

      //[ProjectIteration].[Realese].[BreakChange].[Version]
      Version = "2.0.0.0";
      Description = "UserService is an API that intended to work with users.";
      StartTime = DateTime.UtcNow;
      ApiName = $"HerzenHelper - {_serviceInfoConfig.Name}";
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyName,
          builder =>
          {
            builder
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });

      services.AddHttpContextAccessor();

      string dbConnStr = ConnectionStringHandler.Get(Configuration);

      services.AddDbContext<UserServiceDbContext>(options =>
      {
        options.UseSqlServer(dbConnStr);
      });

      services.AddHealthChecks()
        .AddRabbitMqCheck()
        .AddSqlServer(dbConnStr);

      if (int.TryParse(Environment.GetEnvironmentVariable("MemoryCacheLiveInMinutes"), out int memoryCacheLifetime))
      {
        services.Configure<MemoryCacheConfig>(options =>
        {
          options.CacheLiveInMinutes = memoryCacheLifetime;
        });
      }
      else
      {
        services.Configure<MemoryCacheConfig>(Configuration.GetSection(MemoryCacheConfig.SectionName));
      }

      if (int.TryParse(Environment.GetEnvironmentVariable("RedisCacheLiveInMinutes"), out int redisCacheLifeTime))
      {
        services.Configure<RedisConfig>(options =>
        {
          options.CacheLiveInMinutes = redisCacheLifeTime;
        });
      }
      else
      {
        services.Configure<RedisConfig>(Configuration.GetSection(RedisConfig.SectionName));
      }

      services.Configure<TokenConfiguration>(Configuration.GetSection("CheckTokenMiddleware"));
      services.Configure<BaseServiceInfoConfig>(Configuration.GetSection(BaseServiceInfoConfig.SectionName));
      services.Configure<BaseRabbitMqConfig>(Configuration.GetSection(BaseRabbitMqConfig.SectionName));
      services.Configure<ForwardedHeadersOptions>(options =>
      {
        options.ForwardedHeaders =
          ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
      });

      services.AddBusinessObjects();

      services.AddControllers();

      services.ConfigureMassTransit(_rabbitMqConfig);

      services.AddMemoryCache();

      //TODO this will be used when all validation takes place on the pipeline
      //string path = Path.Combine(
      //    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
      //    "LT.DigitalOffice.UserService.Validation.dll");
      //services.AddScoped<IValidator<JsonPatchDocument<EditUserRequest>>, IEditUserRequestValidator>();

      redisConnStr = services.AddRedisSingleton(Configuration);

      services
        .AddControllers(options =>
        {
          options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
        }) // TODO check enum serialization from request without .AddJsonOptions()
           //this will be used when all validation takes place on the pipeline
           //.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.LoadFrom(path)))
           //.AddFluentValidation()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        })
        .AddNewtonsoftJson();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      app.UpdateDatabase<UserServiceDbContext>();

      FlushRedisDbHelper.FlushDatabase(redisConnStr, Cache.Users);

      app.UseForwardedHeaders();

      app.UseExceptionsHandler(loggerFactory);

      app.UseApiInformation();

      app.UseRouting();

      app.UseMiddleware<TokenMiddleware>();

      app.UseCors(CorsPolicyName);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers().RequireCors(CorsPolicyName);
        endpoints.MapHealthChecks($"/{_serviceInfoConfig.Id}/hc", new HealthCheckOptions
        {
          ResultStatusCodes = new Dictionary<HealthStatus, int>
          {
            { HealthStatus.Unhealthy, 200 },
            { HealthStatus.Healthy, 200 },
            { HealthStatus.Degraded, 200 },
          },
          Predicate = check => check.Name != "masstransit-bus",
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
      });
    }

    #endregion
  }
}
