using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.Gender;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.User.Interfaces;

[AutoInject]
public interface ICreateGenderCommand
{
  Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateGenderRequest request);
}
