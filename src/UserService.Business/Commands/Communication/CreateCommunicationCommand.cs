﻿using FluentValidation.Results;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Helpers.TextHandlers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Enums;
using UniversityHelper.Models.Broker.Responses.TextTemplate;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Commands.Communication.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Mappers.Db.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Configurations;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using UniversityHelper.UserService.Validation.Communication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Communication;

public class CreateCommunicationCommand : ICreateCommunicationCommand
{
  private readonly ICreateCommunicationRequestValidator _validator;
  private readonly IDbUserCommunicationMapper _mapper;
  private readonly IUserCommunicationRepository _communicationRepository;
  private readonly IUserRepository _userRepository;
  //private readonly IAccessValidator _accessValidator;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IResponseCreator _responseCreator;
  private readonly IMemoryCache _cache;
  private readonly IOptions<MemoryCacheConfig> _cacheOptions;
  private readonly ITextTemplateParser _parser;
  private readonly ITextTemplateService _textTemplateService;
  private readonly IEmailService _emailService;

  private async Task NotifyAsync(
    DbUserCommunication dbUserCommunication,
    string secret,
    string locale,
    List<string> errors)
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

  public CreateCommunicationCommand(
    ICreateCommunicationRequestValidator validator,
    IDbUserCommunicationMapper mapper,
    IUserCommunicationRepository communicationRepository,
    IUserRepository userRepository,
  //IAccessValidator accessValidator,
    IHttpContextAccessor httpContextAccessor,
    IResponseCreator responseCreator,
    IMemoryCache cache,
    IOptions<MemoryCacheConfig> cacheOptions,
    ITextTemplateParser parser,
    ITextTemplateService textTemplateService,
    IEmailService emailService)
  {
    _validator = validator;
    _mapper = mapper;
    _communicationRepository = communicationRepository;
    _userRepository = userRepository;
    //_accessValidator = accessValidator;
    _httpContextAccessor = httpContextAccessor;
    _responseCreator = responseCreator;
    _cache = cache;
    _cacheOptions = cacheOptions;
    _textTemplateService = textTemplateService;
    _parser = parser;
    _emailService = emailService;
  }

  public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateCommunicationRequest request)
  {
    //if ((request.UserId != _httpContextAccessor.HttpContext.GetUserId()) &&
    //  !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
    //{
    //  return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
    //}

    ValidationResult validationResult = await _validator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
      return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest,
        validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
    }

    DbUserCommunication dbUserCommunication = _mapper.Map(request);

    OperationResultResponse<Guid?> response = new(
      await _communicationRepository.CreateAsync(dbUserCommunication));

    if (response.Body is null)
    {
      return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
    }

    if (request.Type == CommunicationType.Email)
    {
      string secret = Guid.NewGuid().ToString();

      _cache.Set(dbUserCommunication.Id, secret, TimeSpan.FromMinutes(_cacheOptions.Value.CacheLiveInMinutes));

      await NotifyAsync(dbUserCommunication, secret, "ru", response.Errors);
    }

    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

    return response;
  }
}
