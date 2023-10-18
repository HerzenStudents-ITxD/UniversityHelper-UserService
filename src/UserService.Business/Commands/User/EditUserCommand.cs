using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Constants;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.RedisSupport.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Patch.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User
{
  /// <inheritdoc/>
  public class EditUserCommand : IEditUserCommand
  {
    private readonly IUserRepository _userRepository;
    private readonly IPatchDbUserMapper _mapperUser;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IGlobalCacheRepository _globalCache;

    public EditUserCommand(
      IUserRepository userRepository,
      IPatchDbUserMapper mapperUser,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IGlobalCacheRepository globalCache)
    {
      _userRepository = userRepository;
      _mapperUser = mapperUser;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, JsonPatchDocument<EditUserRequest> patch)
    {
      Guid requestSenderId = _httpContextAccessor.HttpContext.GetUserId();

      bool isAdmin = await _accessValidator.IsAdminAsync(requestSenderId);
      bool isAddEditRemoveUsers = await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers);

      Operation<EditUserRequest> isAdminOperation = patch.Operations.FirstOrDefault(
        o => o.path.EndsWith(nameof(EditUserRequest.IsAdmin), StringComparison.OrdinalIgnoreCase));

      Operation<EditUserRequest> isGenderOperation = patch.Operations.FirstOrDefault(
        o => o.path.EndsWith(nameof(EditUserRequest.GenderId), StringComparison.OrdinalIgnoreCase));

      if ((userId != requestSenderId && !isAddEditRemoveUsers && !isAdmin) ||
        (isAdminOperation is not null && !isAdmin) ||
        (isGenderOperation is not null && userId != requestSenderId))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      OperationResultResponse<bool> response = new();

      (JsonPatchDocument<DbUser> dbUserPatch, JsonPatchDocument<DbUserAddition> dbUserAdditionPatch) = _mapperUser.Map(patch);

      if (dbUserPatch.Operations.Any())
      {
        response.Body = await _userRepository.EditUserAsync(userId, dbUserPatch);

        if (!response.Body)
        {
          return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
        }

        await _globalCache.RemoveAsync(userId);
      }

      if (dbUserAdditionPatch.Operations.Any())
      {
        response.Body = await _userRepository.EditUserAdditionAsync(userId, dbUserAdditionPatch);

        if (!response.Body)
        {
          return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
        }
      }

      return response;
    }
  }
}
