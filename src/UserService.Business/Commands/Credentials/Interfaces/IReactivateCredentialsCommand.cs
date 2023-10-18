using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Requests.Credentials;
using HerzenHelper.UserService.Models.Dto.Responses.Credentials;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Credentials.Interfaces
{
  [AutoInject]
  public interface IReactivateCredentialsCommand
  {
    Task<OperationResultResponse<CredentialsResponse>> ExecuteAsync(ReactivateCredentialsRequest request);
  }
}
