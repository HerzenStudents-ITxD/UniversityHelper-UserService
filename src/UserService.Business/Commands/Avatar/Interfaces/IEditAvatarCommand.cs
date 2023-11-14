using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Avatar.Interfaces;

[AutoInject]
public interface IEditAvatarCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, Guid imageId);
}
