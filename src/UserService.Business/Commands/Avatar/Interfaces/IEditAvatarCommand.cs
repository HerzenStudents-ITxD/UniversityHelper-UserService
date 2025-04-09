using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.UserService.Business.Commands.Avatar.Interfaces;

[AutoInject]
public interface IEditAvatarCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, Guid imageId);
}
