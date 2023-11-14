using FluentValidation.Results;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Constants;
using UniversityHelper.Core.Extensions;
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
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using UniversityHelper.UserService.Validation.Communication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Communication;

public class EditCommunicationCommand : IEditCommunicationCommand
{
  private readonly IUserCommunicationRepository _repository;
  //private readonly IAccessValidator _accessValidator;
  private readonly IEditCommunicationRequestValidator _validator;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IResponseCreator _responseCreator;
  private readonly ITextTemplateService _textTemplateService;
  private readonly IEmailService _emailService;
  private readonly ITextTemplateParser _parser;
  private readonly IMemoryCache _cache;
  private readonly IOptions<MemoryCacheConfig> _cacheOptions;

  private async Task NotifyAsync(
    DbUserCommunication dbUserCommunication,
    string secret,
    string locale,
    List<string> errors)
  {
    IGetTextTemplateResponse textTemplate = await _textTemplateService
      .GetAsync(TemplateType.ConfirmСommunication, locale, errors);

    if (textTemplate is null)
    {
      return;
    }

    string parsedText = _parser.Parse(
      new Dictionary<string, string> { { "Secret", secret } },
      _parser.ParseModel<DbUserCommunication>(dbUserCommunication, textTemplate.Text));

    await _emailService.SendAsync(dbUserCommunication.Value, textTemplate.Subject, parsedText, errors);
  }

  public EditCommunicationCommand(
    IUserCommunicationRepository repository,
    //IAccessValidator accessValidator,
    IEditCommunicationRequestValidator validator,
    IHttpContextAccessor httpContextAccessor,
    IResponseCreator responseCreator,
    ITextTemplateService textTemplateService,
    IEmailService emailService,
    ITextTemplateParser parser,
    IMemoryCache cache,
    IOptions<MemoryCacheConfig> cacheOptions)
  {
    _repository = repository;
    //_accessValidator = accessValidator;
    _validator = validator;
    _httpContextAccessor = httpContextAccessor;
    _responseCreator = responseCreator;
    _textTemplateService = textTemplateService;
    _emailService = emailService;
    _parser = parser;
    _cache = cache;
    _cacheOptions = cacheOptions;
  }
  public async Task<OperationResultResponse<bool>> ExecuteAsync(
    Guid communicationId,
    EditCommunicationRequest request)
  {
    DbUserCommunication dbUserCommunication = await _repository
      .GetAsync(communicationId);

    if (dbUserCommunication is null)
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
    }

    //if (_httpContextAccessor.HttpContext.GetUserId() != dbUserCommunication.UserId &&
    //  !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
    //{
    //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
    //}

    ValidationResult validationResult = await _validator
      .ValidateAsync((dbUserCommunication, request));

    if (!validationResult.IsValid)
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest,
        validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
    }

    OperationResultResponse<bool> response = new();

    if (request.Type is not null)
    {
      await _repository.RemoveBaseTypeAsync(dbUserCommunication.UserId);
      await _repository.SetBaseTypeAsync(communicationId, _httpContextAccessor.HttpContext.GetUserId());
    }
    else
    {
      await _repository.EditAsync(communicationId, request.Value);

      if (dbUserCommunication.Type == (int)CommunicationType.Email)
      {
        Guid secret = Guid.NewGuid();
        _cache.Set(dbUserCommunication.Id, secret, TimeSpan.FromMinutes(_cacheOptions.Value.CacheLiveInMinutes));

        await NotifyAsync(dbUserCommunication, secret.ToString(), "ru", response.Errors);
      }
    }

    response.Body = true;
    return response;
  }
}
