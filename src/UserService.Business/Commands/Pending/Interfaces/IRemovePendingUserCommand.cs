using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Pending.Interfaces;

[AutoInject]
public interface IRemovePendingUserCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId);
}
