using FluentValidation.Results;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Constants;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Helpers.TextHandlers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Enums;
using UniversityHelper.Models.Broker.Responses.TextTemplate;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Commands.Password.Interfaces;
using UniversityHelper.UserService.Business.Commands.User.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.User;
using UniversityHelper.UserService.Models.Dto.Requests.User.Filters;
using UniversityHelper.UserService.Validation.User.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.User
{
  public class EditUserActiveCommand : IEditUserActiveCommand
  {
    //private readonly IEditUserActiveRequestValidator _validator;
    private readonly IUserRepository _userRepository;
    private readonly IUserCredentialsRepository _userCredentialsRepository;
    private readonly IUserCommunicationRepository _userCommunicationRepository;
    private readonly IUserAvatarRepository _userAvatarRepository;
    private readonly IPendingUserRepository _pendingRepository;
    private readonly IGeneratePasswordCommand _generatePassword;
    //private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly ITextTemplateService _textTemplateService;
    private readonly IEmailService _emailService;
    private readonly ITextTemplateParser _parser;
    private readonly IGlobalCacheRepository _globalCache;

    private async Task NotifyAsync(
      DbUser dbUser,
      string email,
      string password,
      string locale,
      List<string> errors)
    {
      IGetTextTemplateResponse textTemplate = await _textTemplateService.GetAsync(
        await _userCredentialsRepository.DoesExistAsync(dbUser.Id) ? TemplateType.PasswordRecovery : TemplateType.Greeting,
        locale,
        errors);

      if (textTemplate is null)
      {
        return;
      }

      string parsedText = _parser.Parse(
        new Dictionary<string, string> { { "Password", password } },
        _parser.ParseModel<DbUser>(dbUser, textTemplate.Text));

      await _emailService.SendAsync(email: email, subject: textTemplate.Subject, text: parsedText, errors);
    }

    public EditUserActiveCommand(
      //IEditUserActiveRequestValidator validator,
      IUserRepository userRepository,
      IUserCredentialsRepository userCredentialsRepository,
      IUserCommunicationRepository userCommunicationRepository,
      IUserAvatarRepository userAvatarRepository,
      IPendingUserRepository pendingRepository,
      IGeneratePasswordCommand generatePassword,
      //IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      ITextTemplateService textTemplateService,
      IEmailService emailService,
      ITextTemplateParser parser,
      IGlobalCacheRepository globalCache)
    {
      //_validator = validator;
      _userRepository = userRepository;
      _userCredentialsRepository = userCredentialsRepository;
      _userCommunicationRepository = userCommunicationRepository;
      _userAvatarRepository = userAvatarRepository;
      _pendingRepository = pendingRepository;
      _generatePassword = generatePassword;
      //_accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _textTemplateService = textTemplateService;
      _emailService = emailService;
      _parser = parser;
      _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(EditUserActiveRequest request)
    {
      DbUser dbUser = await _userRepository
        .GetAsync(new GetUserFilter() { UserId = request.UserId, IncludeCommunications = true });

      DbUser dbRequestSender = await _userRepository.GetAsync(_httpContextAccessor.HttpContext.GetUserId());

      //if (dbRequestSender.Id == request.UserId
      //  || (dbUser.IsAdmin && !dbRequestSender.IsAdmin)
      //  || (!dbRequestSender.IsAdmin
      //    && !await _accessValidator.HasRightsAsync(userId: dbRequestSender.Id, includeIsAdminCheck: false, Rights.AddEditRemoveUsers)))
      //{
      //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      //}

      //ValidationResult validationResult = await _validator
      //  .ValidateAsync((dbUser, request));

      //if (!validationResult.IsValid)
      //{
      //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest,
      //    validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      //}

      OperationResultResponse<bool> response = new();

      if (!request.IsActive)
      {
        if (!await _userRepository.SwitchActiveStatusAsync(request.UserId, request.IsActive))
        {
          return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
        }

        await _userCommunicationRepository.RemoveBaseTypeAsync(request.UserId);

        //TODO

        response.Body = true;
      }
      else
      {
        string password = _generatePassword.Execute();

        await _pendingRepository.CreateAsync(
          new DbPendingUser()
          {
            UserId = request.UserId,
            Password = password,
            CommunicationId = request.CommunicationId.Value
          });

        response.Body = true;

        await NotifyAsync(
          dbUser,
          dbUser.Communications.FirstOrDefault(c => c.Id == request.CommunicationId)?.Value,
          password,
          "ru",
          response.Errors);
      }

      if (response.Body)
      {
        await _globalCache.RemoveAsync(request.UserId);
      }

      return response;
    }
  }
}
