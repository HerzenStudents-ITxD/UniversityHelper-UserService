using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Communication.Interfaces;

[AutoInject]
public interface IConfirmCommunicationCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(Guid communicationId, string secret);
}
