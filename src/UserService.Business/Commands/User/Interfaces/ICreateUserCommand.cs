using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Interfaces;

[AutoInject]
public interface ICreateUserCommand
{
  Task<OperationResultResponse<Guid>> ExecuteAsync(CreateUserRequest request);
}
