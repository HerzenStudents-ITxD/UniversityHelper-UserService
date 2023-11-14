using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Communication.Interfaces;

[AutoInject]
public interface IEditCommunicationCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(Guid communicationId, EditCommunicationRequest request);
}
