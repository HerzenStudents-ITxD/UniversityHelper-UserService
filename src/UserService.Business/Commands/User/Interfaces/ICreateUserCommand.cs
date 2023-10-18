using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Interfaces
{
  [AutoInject]
  public interface ICreateUserCommand
  {
    Task<OperationResultResponse<Guid>> ExecuteAsync(CreateUserRequest request);
  }
}
