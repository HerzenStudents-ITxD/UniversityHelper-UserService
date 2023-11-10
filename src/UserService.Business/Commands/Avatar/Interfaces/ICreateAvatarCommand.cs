using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.Avatar;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Avatar.Interfaces
{
  [AutoInject]
  public interface ICreateAvatarCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateAvatarRequest request);
  }
}
