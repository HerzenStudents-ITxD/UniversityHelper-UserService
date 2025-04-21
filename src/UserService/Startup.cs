using UniversityHelper.Core.RedisSupport.Extensions;
using HealthChecks.UI.Client;
using UniversityHelper.Core.BrokerSupport.Broker.Consumer;
using UniversityHelper.Core.BrokerSupport.Configurations;
using UniversityHelper.Core.BrokerSupport.Extensions;
using UniversityHelper.Core.BrokerSupport.Middlewares.Token;
using UniversityHelper.Core.BrokerSupport.Helpers;
using UniversityHelper.Core.Configurations;
using UniversityHelper.Core.EFSupport.Extensions;
using UniversityHelper.Core.EFSupport.Helpers;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Middlewares.ApiInformation;
using UniversityHelper.Core.RedisSupport.Configurations;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Helpers;
using UniversityHelper.UserService.Broker.Consumers;
using UniversityHelper.UserService.Data.Provider.MsSql.Ef;
using UniversityHelper.UserService.Models.Dto.Configurations;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using MassTransit;
using MassTransit.RabbitMqTransport;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.UserService;

public class Startup : BaseApiInfo
{
    public const string CorsPolicyName = "LtDoCorsPolicy";
    private string redisConnStr;
    private readonly BaseServiceInfoConfig _serviceInfoConfig;
    private readonly RabbitMqConfig _rabbitMqConfig;
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        _serviceInfoConfig = Configuration
            .GetSection(BaseServiceInfoConfig.SectionName)
            .Get<BaseServiceInfoConfig>();
        _rabbitMqConfig = Configuration
            .GetSection(BaseRabbitMqConfig.SectionName)
            .Get<RabbitMqConfig>();
        Version = "2.0.2.0";
        Description = "UserService is an API that intended to work with users.";
        StartTime = DateTime.UtcNow;
        ApiName = $"UniversityHelper - {_serviceInfoConfig.Name}";
    }

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

    private void ConfigureMassTransit(IServiceCollection services)
    {
        (string username, string password) = RabbitMqCredentialsHelper
            .Get(_rabbitMqConfig, _serviceInfoConfig);

        services.AddMassTransit(x =>
        {
            // Register consumers
            x.AddConsumer<AccessValidatorConsumer>();
            x.AddConsumer<GetUsersDataConsumer>();
            x.AddConsumer<UserLoginConsumer>();
            x.AddConsumer<SearchUsersConsumer>();
            x.AddConsumer<CreateAdminConsumer>();
            x.AddConsumer<FindParseEntitiesConsumer>();
            x.AddConsumer<CheckUsersExistenceConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(_rabbitMqConfig.Host, "/", host =>
                {
                    host.Username(username);
                    host.Password(password);
                });

                // Configure endpoints
                cfg.ReceiveEndpoint(_rabbitMqConfig.CheckUserIsAdminEndpoint, e =>
                {
                    e.ConfigureConsumer<AccessValidatorConsumer>(context);
                });

                cfg.ReceiveEndpoint(_rabbitMqConfig.GetUsersDataEndpoint, e =>
                {
                    e.ConfigureConsumer<GetUsersDataConsumer>(context);
                });

                cfg.ReceiveEndpoint(_rabbitMqConfig.GetUserCredentialsEndpoint, e =>
                {
                    e.ConfigureConsumer<UserLoginConsumer>(context);
                });

                cfg.ReceiveEndpoint(_rabbitMqConfig.SearchUsersEndpoint, e =>
                {
                    e.ConfigureConsumer<SearchUsersConsumer>(context);
                });

                cfg.ReceiveEndpoint(_rabbitMqConfig.CreateAdminEndpoint, e =>
                {
                    e.ConfigureConsumer<CreateAdminConsumer>(context);
                });

                cfg.ReceiveEndpoint(_rabbitMqConfig.FindParseEntitiesEndpoint, e =>
                {
                    e.ConfigureConsumer<FindParseEntitiesConsumer>(context);
                });

                cfg.ReceiveEndpoint(_rabbitMqConfig.CheckUsersExistenceEndpoint, e =>
                {
                    e.ConfigureConsumer<CheckUsersExistenceConsumer>(context);
                });
            });
        });

        services.AddMassTransitHostedService();
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
                        .WithOrigins(
                            "http://localhost:5173",
                            "http://localhost:4200",
                            "http://localhost:4500")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
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
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        services.AddBusinessObjects();
        ConfigureMassTransit(services);

        services.AddMemoryCache();
        redisConnStr = services.AddRedisSingleton(Configuration);

        services
            .AddControllers(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddNewtonsoftJson();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc($"{Version}", new OpenApiInfo
            {
                Version = Version,
                Title = _serviceInfoConfig.Name,
                Description = Description
            });
            options.EnableAnnotations();
        });
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
        app.UseSwagger()
           .UseSwaggerUI(options =>
           {
               options.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{Version}");
           });
    }
}