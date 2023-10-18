using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Broker.Requests.Interfaces;
using HerzenHelper.UserService.Business.Commands.Avatar.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Responses.Interfaces;
using HerzenHelper.UserService.Models.Dto.Responses.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Avatar
{
  public class GetAvatarsCommand : IGetAvatarsCommand
  {
    private readonly IUserAvatarRepository _avatarRepository;
    private readonly IImageService _imageService;
    private readonly IUserImagesResponseMapper _mapper;

    public GetAvatarsCommand(
      IUserAvatarRepository avatarRepository,
      IImageService imageService,
      IUserImagesResponseMapper mapper)
    {
      _avatarRepository = avatarRepository;
      _imageService = imageService;
      _mapper = mapper;
    }

    public async Task<OperationResultResponse<UserImagesResponse>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
      List<Guid> dbImagesIds = await _avatarRepository.GetAvatarsByUserId(userId, cancellationToken);

      OperationResultResponse<UserImagesResponse> response = new();

      if (dbImagesIds is null || !dbImagesIds.Any())
      {
        response.Body = _mapper.Map(userId, await _imageService.GetImagesAsync(dbImagesIds, response.Errors, cancellationToken));
      }

      return response;
    }
  }
}
