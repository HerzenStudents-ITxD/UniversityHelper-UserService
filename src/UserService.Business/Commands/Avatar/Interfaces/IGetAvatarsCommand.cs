using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Responses.Image;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Avatar.Interfaces;

[AutoInject]
public interface IGetAvatarsCommand
{
  Task<OperationResultResponse<UserImagesResponse>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
}
