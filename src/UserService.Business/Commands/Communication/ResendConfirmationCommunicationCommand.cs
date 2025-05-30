﻿using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Helpers.TextHandlers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Enums;
using UniversityHelper.Models.Broker.Responses.TextTemplate;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Commands.Communication.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Configurations;
using UniversityHelper.UserService.Models.Dto.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Communication;

public class ResendConfirmationCommunicationCommand : IResendConfirmationCommunicationCommand
{
  private readonly IUserCommunicationRepository _communicationRepository;
  private readonly IUserRepository _userRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IResponseCreator _responseCreator;
  private readonly IMemoryCache _cache;
  private readonly IOptions<MemoryCacheConfig> _cacheOptions;
  private readonly ITextTemplateService _textTemplateService;
  private readonly IEmailService _emailService;
  private readonly ITextTemplateParser _parser;

  private async Task NotifyAsync(DbUserCommunication dbUserCommunication, string secret, string locale, List<string> errors)
  {

    Task<IGetTextTemplateResponse> textTemplateTask = _textTemplateService
      .GetAsync(TemplateType.ConfirmСommunication, locale, errors);

    Task<DbUser> dbUserTask = _userRepository.GetAsync(dbUserCommunication.UserId);

    IGetTextTemplateResponse textTemplate = await textTemplateTask;
    DbUser dbUser = await dbUserTask;

    if (textTemplate is null)
    {
      return;
    }

    string parsedText = _parser.Parse(
      new Dictionary<string, string> { { "Secret", secret }, { "CommunicationId", dbUserCommunication.Id.ToString() } },
      _parser.ParseModel<DbUser>(dbUser, textTemplate.Text));

    await _emailService.SendAsync(dbUserCommunication.Value, textTemplate.Subject, parsedText, errors);
  }

  public ResendConfirmationCommunicationCommand(
    IUserCommunicationRepository communicationRepository,
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor,
    IResponseCreator responseCreator,
    IMemoryCache cache,
    IOptions<MemoryCacheConfig> cacheOptions,
    ITextTemplateService textTemplateService,
    IEmailService emailService,
    ITextTemplateParser parser)
  {
    _communicationRepository = communicationRepository;
    _userRepository = userRepository;
    _httpContextAccessor = httpContextAccessor;
    _responseCreator = responseCreator;
    _cache = cache;
    _cacheOptions = cacheOptions;
    _textTemplateService = textTemplateService;
    _emailService = emailService;
    _parser = parser;
  }

  public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid communicationId)
  {
    DbUserCommunication dbUserCommunication = await _communicationRepository.GetAsync(communicationId);

    if (dbUserCommunication is null)
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
    }

    if (_httpContextAccessor.HttpContext.GetUserId() != dbUserCommunication.UserId)
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
    }

    if (dbUserCommunication.Type != (int)CommunicationType.Email)
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
    }

    OperationResultResponse<bool> response = new();

    string secret = Guid.NewGuid().ToString();

    _cache.Set(communicationId, secret, TimeSpan.FromMinutes(_cacheOptions.Value.CacheLiveInMinutes));

    await NotifyAsync(dbUserCommunication, secret, "ru", response.Errors);

    response.Body = response.Errors.Any() ? false : true;

    return response;
  }
}
