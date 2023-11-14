using FluentValidation.Results;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Responses.Auth;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Commands.Credentials.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Mappers.Db.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;
using UniversityHelper.UserService.Models.Dto.Responses.Credentials;
using UniversityHelper.UserService.Validation.Credentials.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Credentials;

public class CreateCredentialsCommand : ICreateCredentialsCommand
{
  private readonly IDbUserCredentialsMapper _mapper;
  private readonly IUserRepository _userRepository;
  private readonly IPendingUserRepository _pendingUserRepository;
  private readonly IUserCredentialsRepository _userCredentialsRepository;
  private readonly IUserCommunicationRepository _communicationRepository;
  private readonly IAuthService _authService;
  private readonly ICreateCredentialsRequestValidator _validator;
  private readonly IResponseCreator _responseCreator;
  private readonly IGlobalCacheRepository _globalCache;

  public CreateCredentialsCommand(
    IDbUserCredentialsMapper mapper,
    IUserRepository userRepository,
    IPendingUserRepository pendingUserRepository,
    IUserCredentialsRepository userCredentialsRepository,
    IUserCommunicationRepository communicationRepository,
    IAuthService authService,
    ICreateCredentialsRequestValidator validator,
    IResponseCreator responseCreator,
    IGlobalCacheRepository globalCache)
  {
    _mapper = mapper;
    _userRepository = userRepository;
    _pendingUserRepository = pendingUserRepository;
    _userCredentialsRepository = userCredentialsRepository;
    _communicationRepository = communicationRepository;
    _authService = authService;
    _validator = validator;
    _responseCreator = responseCreator;
    _globalCache = globalCache;
  }

  public async Task<OperationResultResponse<CredentialsResponse>> ExecuteAsync(CreateCredentialsRequest request)
  {
    ValidationResult validationResult = await _validator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
      return _responseCreator.CreateFailureResponse<CredentialsResponse>(
        HttpStatusCode.BadRequest,
        validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
    }

    List<string> errors = new();

    IGetTokenResponse tokenResponse = await _authService.GetTokenAsync(request.UserId, errors);

    if (tokenResponse is null)
    {
      return _responseCreator.CreateFailureResponse<CredentialsResponse>(
        HttpStatusCode.ServiceUnavailable,
        errors);
    }

    await _userCredentialsRepository.CreateAsync(_mapper.Map(request));
    DbPendingUser dbPendingUser = await _pendingUserRepository.RemoveAsync(request.UserId);
    await _userRepository.SwitchActiveStatusAsync(request.UserId, true);
    await _communicationRepository.SetBaseTypeAsync(dbPendingUser.CommunicationId, request.UserId);

    await _globalCache.Clear();

    return new()
    {
      Body = new CredentialsResponse
      {
        UserId = request.UserId,
        AccessToken = tokenResponse.AccessToken,
        RefreshToken = tokenResponse.RefreshToken,
        AccessTokenExpiresIn = tokenResponse.AccessTokenExpiresIn,
        RefreshTokenExpiresIn = tokenResponse.RefreshTokenExpiresIn
      }
    };
  }
}
