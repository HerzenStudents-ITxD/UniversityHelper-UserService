﻿using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Pending.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Pending;

public class RemovePendingUserCommand : IRemovePendingUserCommand
{
  //private readonly IAccessValidator _accessValidator;
  private readonly IResponseCreator _responseCreator;
  private readonly IPendingUserRepository _repository;
  private readonly IGlobalCacheRepository _globalCache;

  public RemovePendingUserCommand(
    //IAccessValidator accessValidator,
    IResponseCreator responseCreator,
    IPendingUserRepository repository,
    IGlobalCacheRepository globalCache)
  {
    //_accessValidator = accessValidator;
    _responseCreator = responseCreator;
    _repository = repository;
    _globalCache = globalCache;
  }

  public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId)
  {
    //if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
    //{
    //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
    //}

    OperationResultResponse<bool> response = new();
    response.Body = (await _repository.RemoveAsync(userId)) is not null;

    if (response.Body)
    {
      await _globalCache.RemoveAsync(userId);
    }

    return response.Body
      ? response
      : _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
  }
}
