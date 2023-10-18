using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Requests.User;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User.Interfaces
{
  [AutoInject]
  public interface IEditUserActiveCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(EditUserActiveRequest request);
  }
}
