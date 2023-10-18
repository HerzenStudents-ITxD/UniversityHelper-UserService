using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Avatar.Interfaces
{
  [AutoInject]
  public interface IEditAvatarCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, Guid imageId);
  }
}
