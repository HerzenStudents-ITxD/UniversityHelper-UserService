using HerzenHelper.Core.FluentValidationExtensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.Core.Validators.Interfaces;
using HerzenHelper.UserService.Broker.Requests.Interfaces;
using HerzenHelper.UserService.Business.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Requests.Filtres;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User
{
  public class FindUserCommand : IFindUserCommand
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoMapper _userInfoMapper;
    private readonly IImageService _imageService;
    private readonly IResponseCreator _responseCreator;

    public FindUserCommand(
      IUserRepository userRepository,
      IUserInfoMapper userInfoMapper,
      IImageService imageService,
      IResponseCreator responseCreator)
    {
      _userRepository = userRepository;
      _userInfoMapper = userInfoMapper;
      _imageService = imageService;
      _responseCreator = responseCreator;
    }

    public async Task<FindResultResponse<UserInfo>> ExecuteAsync(FindUsersFilter filter, CancellationToken cancellationToken = default)
    {
      //TODO
      //if (!_baseFindValidator.ValidateCustom(filter, out List<string> errors))
      //{
      //  return _responseCreator.CreateFailureFindResponse<UserInfo>(HttpStatusCode.BadRequest, errors);
      //}

      FindResultResponse<UserInfo> response = new();

      (List<DbUser> dbUsers, int totalCount) = await _userRepository.FindAsync(filter, cancellationToken: cancellationToken);

      List<ImageInfo> images = filter.IncludeCurrentAvatar
        ? await _imageService.GetImagesAsync(
          dbUsers
            .Where(u => u.Avatars.Any()).Select(u => u.Avatars.FirstOrDefault())
            .Select(ua => ua.AvatarId)
            .ToList(),
          response.Errors,
          cancellationToken)
        : default;

      response.Body = new();
      response.Body
        .AddRange(dbUsers.Select(dbUser =>
        _userInfoMapper.Map(
          dbUser,
          images?.FirstOrDefault(i => i.Id == dbUser.Avatars.FirstOrDefault()?.AvatarId)
        )));

      response.TotalCount = totalCount;

      return response;
    }
  }
}
