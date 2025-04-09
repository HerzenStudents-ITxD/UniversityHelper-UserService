using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.Avatar;

namespace UniversityHelper.UserService.Business.Commands.Avatar.Interfaces;

[AutoInject]
public interface IRemoveAvatarsCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(RemoveAvatarsRequest request);
}
