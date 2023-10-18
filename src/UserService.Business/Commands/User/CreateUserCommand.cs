using FluentValidation;
using FluentValidation.Results;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Constants;
using HerzenHelper.Core.FluentValidationExtensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Helpers.TextHandlers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.Models.Broker.Enums;
using HerzenHelper.Models.Broker.Responses.TextTemplate;
using HerzenHelper.UserService.Broker.Requests.Interfaces;
using HerzenHelper.UserService.Business.Commands.Password.Interfaces;
using HerzenHelper.UserService.Business.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Db.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto;
using HerzenHelper.UserService.Models.Dto.Enums;
using HerzenHelper.UserService.Validation.User.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User
{
  /// <inheritdoc/>
  public class CreateUserCommand : ICreateUserCommand
  {
    private readonly ICreateUserRequestValidator _validator;
    private readonly IUserRepository _userRepository;
    private readonly IPendingUserRepository _pendingUserRepository;
    private readonly IUserAvatarRepository _avatarRepository;
    private readonly IDbUserMapper _dbUserMapper;
    private readonly IDbUserAvatarMapper _dbUserAvatarMapper;
    private readonly IAccessValidator _accessValidator;
    private readonly IGeneratePasswordCommand _generatePassword;
    private readonly IResponseCreator _responseCreator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITextTemplateParser _parser;
    private readonly IImageService _imageService;
    private readonly ITextTemplateService _textTemplateService;
    private readonly IEmailService _emailService;

    private async Task NotifyAsync(
      DbUser dbUser,
      string password,
      string locale,
      List<string> errors)
    {
      IGetTextTemplateResponse textTemplate = await _textTemplateService
        .GetAsync(TemplateType.Greeting, locale, errors);

      if (textTemplate is null)
      {
        return;
      }

      string parsedText = _parser.Parse(
        new Dictionary<string, string> { { "Password", password } },
        _parser.ParseModel<DbUser>(dbUser, textTemplate.Text));

      string email = dbUser.Communications
        .FirstOrDefault(c => c.Type == (int)CommunicationType.Email)?.Value;

      await _emailService.SendAsync(email: email, subject: textTemplate.Subject, text: parsedText, errors);
    }

    public CreateUserCommand(
      ICreateUserRequestValidator validator,
      IUserRepository userRepository,
      IPendingUserRepository pendingUserRepository,
      IUserAvatarRepository avatarRepository,
      IHttpContextAccessor httpContextAccessor,
      IDbUserMapper dbUserMapper,
      IDbUserAvatarMapper dbUserAvatarMapper,
      IAccessValidator accessValidator,
      IGeneratePasswordCommand generatePassword,
      IResponseCreator responseCreator,
      ITextTemplateParser parser,
      IImageService imageService,
      IEmailService emailService,
      ITextTemplateService textTemplateService)
    {
      _validator = validator;
      _userRepository = userRepository;
      _pendingUserRepository = pendingUserRepository;
      _avatarRepository = avatarRepository;
      _dbUserMapper = dbUserMapper;
      _dbUserAvatarMapper = dbUserAvatarMapper;
      _accessValidator = accessValidator;
      _generatePassword = generatePassword;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _parser = parser;
      _imageService = imageService;
      _emailService = emailService;
      _textTemplateService = textTemplateService;
    }

    public async Task<OperationResultResponse<Guid>> ExecuteAsync(CreateUserRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      DbUser dbUser = _dbUserMapper.Map(request);

      OperationResultResponse<Guid> response = new(body: dbUser.Id);

      string password = !string.IsNullOrEmpty(request.Password?.Trim()) ?
        request.Password.Trim() : _generatePassword.Execute();

      await _userRepository.CreateAsync(dbUser);

      await _pendingUserRepository
        .CreateAsync(new DbPendingUser()
        {
          UserId = dbUser.Id,
          Password = password,
          CommunicationId = dbUser.Communications.FirstOrDefault().Id
        });

      Guid? avatarImageId = request.AvatarImage is null
        ? null
        : await _imageService.CreateImageAsync(request.AvatarImage, response.Errors);

      if (avatarImageId.HasValue)
      {
        await _avatarRepository.CreateAsync(_dbUserAvatarMapper.Map(avatarId: avatarImageId.Value, userId: dbUser.Id, isCurrentAvatar: true));
      }

      await Task.WhenAll(
        NotifyAsync(dbUser, password: password, locale: "ru", response.Errors)

        //TODO
        //request.RoleId.HasValue
        //  ? _publish.CreateUserRoleAsync(userId: dbUser.Id, roleId: request.RoleId.Value)
        //  : Task.CompletedTask,
        );

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
