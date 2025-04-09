using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Responses.Auth;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Commands.Credentials.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Mappers.Helpers.Password;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials.Filters;
using UniversityHelper.UserService.Models.Dto.Responses.Credentials;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Credentials;

public class ReactivateCredentialsCommand : IReactivateCredentialsCommand
{
  private readonly IPendingUserRepository _pendingRepository;
  private readonly IUserCredentialsRepository _credentialsRepository;
  private readonly IUserRepository _userRepository;
  private readonly IUserCommunicationRepository _communicationRepository;
  private readonly IResponseCreator _responseCreator;
  private readonly IAuthService _authService;
  private readonly IGlobalCacheRepository _globalCache;

  public ReactivateCredentialsCommand(
    IPendingUserRepository pendingRepository,
    IUserCredentialsRepository credentialsRepository,
    IUserRepository userRepository,
    IUserCommunicationRepository communicationRepository,
    IResponseCreator responseCreator,
    IAuthService authService,
    IGlobalCacheRepository globalCache)
  {
    _pendingRepository = pendingRepository;
    _credentialsRepository = credentialsRepository;
    _userRepository = userRepository;
    _communicationRepository = communicationRepository;
    _responseCreator = responseCreator;
    _authService = authService;
    _globalCache = globalCache;
  }

  public async Task<OperationResultResponse<CredentialsResponse>> ExecuteAsync(ReactivateCredentialsRequest request)
  {
    DbPendingUser dbPendingUser = await _pendingRepository.GetAsync(request.UserId);

    DbUserCredentials dbUserCredentials = await _credentialsRepository
      .GetAsync(new GetCredentialsFilter() { UserId = request.UserId, IncludeDeactivated = true });

    if (dbPendingUser is null || dbUserCredentials is null || dbPendingUser.Password != request.Password)
    {
      return _responseCreator.CreateFailureResponse<CredentialsResponse>(HttpStatusCode.BadRequest);
    }

    OperationResultResponse<CredentialsResponse> response = new();

    IGetTokenResponse tokenResponse = await _authService.GetTokenAsync(request.UserId, response.Errors);

    if (tokenResponse is null)
    {
      response.Errors.Add("Something is wrong, please try again later.");

      return response;
    }

    dbUserCredentials.PasswordHash = UserPasswordHash.GetPasswordHash(
      dbUserCredentials.Login,
      dbUserCredentials.Salt,
      request.Password);

    await _credentialsRepository.EditAsync(dbUserCredentials);
    await _pendingRepository.RemoveAsync(request.UserId);
    await _userRepository.SwitchActiveStatusAsync(request.UserId, true);
    await _communicationRepository.SetBaseTypeAsync(dbPendingUser.CommunicationId, request.UserId);

    await _globalCache.Clear();

    response.Body = new CredentialsResponse
    {
      UserId = request.UserId,
      AccessToken = tokenResponse.AccessToken,
      RefreshToken = tokenResponse.RefreshToken,
      AccessTokenExpiresIn = tokenResponse.AccessTokenExpiresIn,
      RefreshTokenExpiresIn = tokenResponse.RefreshTokenExpiresIn
    };

    return response;
  }
}
