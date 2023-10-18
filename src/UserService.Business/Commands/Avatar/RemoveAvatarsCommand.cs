using FluentValidation.Results;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Constants;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.RedisSupport.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Avatar.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Models.Dto.Requests.Avatar;
using HerzenHelper.UserService.Validation.Image.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Avatar
{
  public class RemoveAvatarsCommand : IRemoveAvatarsCommand
  {
    private readonly IUserAvatarRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRemoveAvatarsRequestValidator _validator;
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly IGlobalCacheRepository _globalCache;

    public RemoveAvatarsCommand(
      IUserAvatarRepository repository,
      IHttpContextAccessor httpContextAccessor,
      IRemoveAvatarsRequestValidator validator,
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      IGlobalCacheRepository globalCache)
    {
      _repository = repository;
      _httpContextAccessor = httpContextAccessor;
      _validator = validator;
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(RemoveAvatarsRequest request)
    {
      if (request.UserId != _httpContextAccessor.HttpContext.GetUserId()
        && !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage).ToList());
      }
      OperationResultResponse<bool> response = new();

      response.Body = await _repository.RemoveAsync(request.AvatarsIds);

      if (response.Body)
      {
        await _globalCache.RemoveAsync(request.UserId);
      }
      else
      {
        response = _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }

      return response;
    }
  }
}
