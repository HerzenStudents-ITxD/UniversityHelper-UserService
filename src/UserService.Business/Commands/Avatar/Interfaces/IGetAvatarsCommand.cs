using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Responses.Image;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Avatar.Interfaces
{
  [AutoInject]
  public interface IGetAvatarsCommand
  {
    Task<OperationResultResponse<UserImagesResponse>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
  }
}
