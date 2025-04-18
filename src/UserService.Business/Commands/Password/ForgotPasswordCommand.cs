﻿using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Helpers.TextHandlers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Enums;
using UniversityHelper.Models.Broker.Responses.TextTemplate;
using UniversityHelper.UserService.Broker.Helpers.Login;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Commands.Password.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Configurations;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Models.Dto.Requests.User.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Password;

public class ForgotPasswordCommand : IForgotPasswordCommand
{
  private readonly IGeneratePasswordCommand _generatePassword;
  private readonly IOptions<MemoryCacheConfig> _cacheOptions;
  private readonly IUserRepository _repository;
  private readonly IMemoryCache _cache;
  private readonly ITextTemplateParser _parser;
  private readonly IResponseCreator _responseCreator;
  private readonly ITextTemplateService _textTemplateService;
  private readonly IEmailService _emailService;

  private GetUserFilter CreateFilter(string LoginData)
  {
    GetUserFilter filter = new();

    if (LoginData.IsEmail())
    {
      filter.Email = LoginData;
    }
    else
    {
      filter.Login = LoginData;
    }

    filter.IncludeCommunications = true;

    return filter;
  }

  private string SetGuidInCache(Guid userId)
  {
    string secret = _generatePassword.Execute();

    _cache.Set(secret, userId, new MemoryCacheEntryOptions
    {
      AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheOptions.Value.CacheLiveInMinutes)
    });

    return secret;
  }

  private async Task NotifyAsync(
    DbUser dbUser,
    string email,
    string secret,
    string locale,
    List<string> errors)
  {
    IGetTextTemplateResponse textTemplate = await _textTemplateService
      .GetAsync(TemplateType.PasswordRecovery, locale, errors);

    if (textTemplate is null)
    {
      return;
    }

    //do not put "secret" into the link! it's not secure
    string parsedText = _parser.Parse(
      new Dictionary<string, string> { { "Password", secret } },
      _parser.ParseModel<DbUser>(dbUser, textTemplate.Text));

    await _emailService.SendAsync(email, textTemplate.Subject, parsedText, errors);
  }

  public ForgotPasswordCommand(
    IGeneratePasswordCommand generatePassword,
    IOptions<MemoryCacheConfig> cacheOptions,
    IUserRepository repository,
    IMemoryCache cache,
    ITextTemplateParser parser,
    IResponseCreator responseCreator,
    ITextTemplateService textTemplateService,
    IEmailService emailService)
  {
    _generatePassword = generatePassword;
    _repository = repository;
    _cacheOptions = cacheOptions;
    _cache = cache;
    _parser = parser;
    _responseCreator = responseCreator;
    _textTemplateService = textTemplateService;
    _emailService = emailService;
  }

  public async Task<OperationResultResponse<string>> ExecuteAsync(string userLoginData)
  {
    if (string.IsNullOrEmpty(userLoginData))
    {
      return _responseCreator.CreateFailureResponse<string>(HttpStatusCode.BadRequest);
    }

    GetUserFilter filter = CreateFilter(userLoginData);

    DbUser dbUser = await _repository.GetAsync(filter);

    if (dbUser is null)
    {
      return _responseCreator.CreateFailureResponse<string>(HttpStatusCode.NotFound);
    }

    string secret = SetGuidInCache(dbUser.Id);
    string email = filter.Email is null
      ? dbUser.Communications.FirstOrDefault(c => c.Type == (int)CommunicationType.BaseEmail).Value
      : filter.Email;

    OperationResultResponse<string> response = new();

    await NotifyAsync(dbUser, email, secret, "ru", response.Errors);

    response.Body = response.Errors.Any() ? null : email;

    return response;
  }
}
