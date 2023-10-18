using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Requests.Avatar;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Avatar.Interfaces
{
  [AutoInject]
  public interface IRemoveAvatarsCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(RemoveAvatarsRequest request);
  }
}
