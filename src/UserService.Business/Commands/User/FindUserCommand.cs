using UniversityHelper.Core.FluentValidationExtensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Core.Validators.Interfaces;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.Filtres;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.User
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
