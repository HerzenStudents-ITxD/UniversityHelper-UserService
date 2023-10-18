﻿using FluentValidation.Results;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Constants;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Helpers.TextHandlers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.Models.Broker.Enums;
using HerzenHelper.Models.Broker.Responses.TextTemplate;
using HerzenHelper.UserService.Broker.Requests.Interfaces;
using HerzenHelper.UserService.Business.Commands.Communication.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Db.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Configurations;
using HerzenHelper.UserService.Models.Dto.Enums;
using HerzenHelper.UserService.Models.Dto.Requests.Communication;
using HerzenHelper.UserService.Validation.Communication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Communication
{
  public class CreateCommunicationCommand : ICreateCommunicationCommand
  {
    private readonly ICreateCommunicationRequestValidator _validator;
    private readonly IDbUserCommunicationMapper _mapper;
    private readonly IUserCommunicationRepository _communicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccessValidator _accessValidator;
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
    IAccessValidator accessValidator,
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
      _accessValidator = accessValidator;
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
      if ((request.UserId != _httpContextAccessor.HttpContext.GetUserId()) &&
        !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

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
}
