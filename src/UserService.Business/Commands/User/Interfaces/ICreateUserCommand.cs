using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto;

namespace UniversityHelper.UserService.Business.Interfaces;

[AutoInject]
public interface ICreateUserCommand
{
  Task<OperationResultResponse<Guid>> ExecuteAsync(CreateUserRequest request);
}
