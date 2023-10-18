using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Core.Exceptions.Models;
using HerzenHelper.Models.Broker.Requests.User;
using HerzenHelper.Models.Broker.Responses.User;
using HerzenHelper.UserService.Broker.Helpers.Login;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Credentials.Filters;
using MassTransit;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Consumers
{
  public class UserLoginConsumer : IConsumer<IGetUserCredentialsRequest>
  {
    private readonly IUserCredentialsRepository _credentialsRepository;

    private GetCredentialsFilter CreateCredentialsFilter(IGetUserCredentialsRequest request)
    {
      GetCredentialsFilter result = new();

      if (request.LoginData.IsEmail())
      {
        result.Email = request.LoginData;
      }
      else if (request.LoginData.IsPhone())
      {
        result.Phone = request.LoginData;
      }
      else
      {
        result.Login = request.LoginData;
      }

      return result;
    }

    private async Task<object> GetUserCredentials(IGetUserCredentialsRequest request)
    {
      DbUserCredentials dbUserCredentials;

      GetCredentialsFilter filter = CreateCredentialsFilter(request);

      dbUserCredentials = await _credentialsRepository.GetAsync(filter);

      if (dbUserCredentials is null)
      {
        throw new NotFoundException($"User credentials was not found.");
      }

      return IGetUserCredentialsResponse.CreateObj(
        dbUserCredentials.UserId,
        dbUserCredentials.PasswordHash,
        dbUserCredentials.Salt,
        dbUserCredentials.Login);
    }

    public UserLoginConsumer(
      IUserCredentialsRepository credentialsRepository)
    {
      _credentialsRepository = credentialsRepository;
    }

    public async Task Consume(ConsumeContext<IGetUserCredentialsRequest> context)
    {
      object response = OperationResultWrapper.CreateResponse(GetUserCredentials, context.Message);

      await context.RespondAsync<IOperationResult<IGetUserCredentialsResponse>>(response);
    }
  }
}
