using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Requests.Gender;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User.Interfaces
{
  [AutoInject]
  public interface ICreateGenderCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateGenderRequest request);
  }
}
