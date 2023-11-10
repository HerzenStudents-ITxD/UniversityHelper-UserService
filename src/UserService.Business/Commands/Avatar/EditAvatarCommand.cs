using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Constants;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Avatar.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Avatar
{
  public class EditAvatarCommand : IEditAvatarCommand
  {
    private readonly IUserAvatarRepository _avatarRepository;
    //private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IGlobalCacheRepository _globalCache;

    public EditAvatarCommand(
      IHttpContextAccessor httpContextAccessor,
      //IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      IUserAvatarRepository avatarRepository,
      IGlobalCacheRepository globalCache)
    {
      _httpContextAccessor = httpContextAccessor;
      //_accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _avatarRepository = avatarRepository;
      _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, Guid imageId)
    {
      //if (_httpContextAccessor.HttpContext.GetUserId() != userId
      //  && !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      //{
      //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      //}

      OperationResultResponse<bool> response = new(
        body: await _avatarRepository.UpdateCurrentAvatarAsync(userId, imageId));

      if (response.Body)
      {
        await _globalCache.RemoveAsync(userId);
      }
      else
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
      }

      return response;
    }
  }
}
