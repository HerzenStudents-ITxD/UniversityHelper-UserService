using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;

namespace UniversityHelper.UserService.Business.Commands.Communication.Interfaces;

[AutoInject]
public interface ICreateCommunicationCommand
{
  Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateCommunicationRequest request);
}
