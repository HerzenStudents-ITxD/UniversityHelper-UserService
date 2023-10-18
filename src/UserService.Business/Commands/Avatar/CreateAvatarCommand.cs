using FluentValidation.Results;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Constants;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.RedisSupport.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Broker.Requests.Interfaces;
using HerzenHelper.UserService.Business.Commands.Avatar.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Db.Interfaces;
using HerzenHelper.UserService.Models.Dto.Requests.Avatar;
using HerzenHelper.UserService.Validation.Image.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Avatar
{
  public class CreateAvatarCommand : ICreateAvatarCommand
  {
    private readonly IUserAvatarRepository _avatarRepository;
    private readonly IAccessValidator _accessValidator;
    private readonly ICreateAvatarRequestValidator _requestValidator;
    private readonly IDbUserAvatarMapper _dbUserAvatarMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageService _imageService;
    private readonly IResponseCreator _responseCreator;
    private readonly IGlobalCacheRepository _globalCache;

    public CreateAvatarCommand(
      IUserAvatarRepository avatarRepository,
      IAccessValidator accessValidator,
      ICreateAvatarRequestValidator requestValidator,
      IDbUserAvatarMapper dbEntityImageMapper,
      IHttpContextAccessor httpContextAccessor,
      IImageService imageService,
      IResponseCreator responseCreator,
      IGlobalCacheRepository globalCache)
    {
      _avatarRepository = avatarRepository;
      _accessValidator = accessValidator;
      _requestValidator = requestValidator;
      _dbUserAvatarMapper = dbEntityImageMapper;
      _httpContextAccessor = httpContextAccessor;
      _imageService = imageService;
      _responseCreator = responseCreator;
      _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateAvatarRequest request)
    {
      if (_httpContextAccessor.HttpContext.GetUserId() != request.UserId
        && !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _requestValidator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage).ToList());
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _imageService.CreateImageAsync(request, response.Errors);

      if (response.Body is not null)
      {
        await _avatarRepository.CreateAsync(
          _dbUserAvatarMapper
            .Map(response.Body.Value, request.UserId.Value, request.IsCurrentAvatar));

        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

        if (request.IsCurrentAvatar)
        {
          await _globalCache.RemoveAsync(request.UserId.Value);
        }
      }
      else
      {
        response = _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      return response;
    }
  }
}
