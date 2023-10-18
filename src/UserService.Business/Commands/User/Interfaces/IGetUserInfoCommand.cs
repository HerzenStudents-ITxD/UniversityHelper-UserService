using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.UserService.Models.Dto.Models;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User.Interfaces
{
  [AutoInject]
  public interface IGetUserInfoCommand
  {
    Task<OperationResultResponse<UserData>> ExecuteAsync();
  }
}
