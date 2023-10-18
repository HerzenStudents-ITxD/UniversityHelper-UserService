using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Requests.Password;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Password.Interfaces
{
  [AutoInject]
  public interface IChangePasswordCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(ChangePasswordRequest request);
  }
}
