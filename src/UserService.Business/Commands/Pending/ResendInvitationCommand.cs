﻿using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Helpers.TextHandlers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Enums;
using UniversityHelper.Models.Broker.Responses.TextTemplate;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Commands.Password.Interfaces;
using UniversityHelper.UserService.Business.Commands.Pending.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Pending;

public class ResendInvitationCommand : IResendInvitationCommand
{
  //private readonly IAccessValidator _accessValidator;
  private readonly IPendingUserRepository _repository;
  private readonly IUserCredentialsRepository _credentialsRepository;
  private readonly IGeneratePasswordCommand _generatePassword;
  private readonly ITextTemplateParser _parser;
  private readonly IResponseCreator _responseCreator;
  private readonly ITextTemplateService _textTemplateService;
  private readonly IEmailService _emailService;

  private async Task NotifyAsync(
    DbPendingUser dbPendingUser,
    string locale,
    List<string> errors)
  {
    IGetTextTemplateResponse textTemplate = await _textTemplateService
      .GetAsync(
        await _credentialsRepository.DoesExistAsync(dbPendingUser.UserId) ? TemplateType.PasswordRecovery : TemplateType.Greeting,
        locale,
        errors);

    if (textTemplate is null)
    {
      return;
    }

    string email = dbPendingUser.User.Communications
      .FirstOrDefault(c => c.Id == dbPendingUser.CommunicationId)
      .Value;

    string parsedText = _parser.Parse(
      new Dictionary<string, string> { { "Password", dbPendingUser.Password } },
      _parser.ParseModel<DbUser>(dbPendingUser.User, textTemplate.Text));

    await _emailService.SendAsync(email, textTemplate.Subject, parsedText, errors);
  }

  public ResendInvitationCommand(
    //IAccessValidator accessValidator,
    IPendingUserRepository repository,
    IUserCredentialsRepository credentialsRepository,
    IGeneratePasswordCommand generatePassword,
    ITextTemplateParser parser,
    IResponseCreator responseCreator,
    ITextTemplateService textTemplateService,
    IEmailService emailService)
  {
    //_accessValidator = accessValidator;
    _repository = repository;
    _credentialsRepository = credentialsRepository;
    _generatePassword = generatePassword;
    _parser = parser;
    _responseCreator = responseCreator;
    _textTemplateService = textTemplateService;
    _emailService = emailService;
  }

  public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, Guid communicationId)
  {
    //if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
    //{
    //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
    //}

    DbPendingUser dbPendingUser = await _repository.GetAsync(userId: userId, includeUser: true);

    if (dbPendingUser is null)
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
    }

    if (dbPendingUser.CommunicationId != communicationId)
    {
      if (dbPendingUser.User.Communications.FirstOrDefault(c => c.Id == communicationId) is not null)
      {
        dbPendingUser.CommunicationId = communicationId;
      }
      else
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }
    }

    dbPendingUser.Password = _generatePassword.Execute();

    OperationResultResponse<bool> response = new();

    await _repository.UpdateAsync(dbPendingUser);

    await NotifyAsync(dbPendingUser, "ru", response.Errors);

    response.Body = !response.Errors.Any();

    return response;
  }
}
