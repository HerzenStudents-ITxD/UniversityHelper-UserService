using FluentValidation;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;
using UniversityHelper.UserService.Validation.Credentials.Interfaces;
using UniversityHelper.UserService.Validation.Credentials.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace UniversityHelper.UserService.Validation.Credentials;

public class CreateCredentialsRequestValidator : AbstractValidator<CreateCredentialsRequest>, ICreateCredentialsRequestValidator
{
  private static Regex loginRegex = new(@"^([a-zA-Z]+)$|^([a-zA-Z0-9]*[0-9]+[a-zA-Z]+[0-9]*)$|^([a-zA-Z]+[0-9]+)$");

  public CreateCredentialsRequestValidator(
    IPendingUserRepository repository,
    IUserCredentialsRepository credentialsRepository)
  {
    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

    RuleFor(request => request.Login.Trim())
      .MinimumLength(5).WithMessage(CreateCredentialsRequestValidationResource.LoginShort)
      .MaximumLength(15).WithMessage(CreateCredentialsRequestValidationResource.LoginLong)
      .Must(login => loginRegex.IsMatch(login))
      .WithMessage(CreateCredentialsRequestValidationResource.LoginMatch);

    RuleFor(request => request.UserId)
      .NotEmpty().WithMessage(CreateCredentialsRequestValidationResource.UserId);

    RuleFor(request => request)
      .Cascade(CascadeMode.Stop)
      .MustAsync(async (r, _) => !await credentialsRepository.DoesExistAsync(r.UserId))
      .WithMessage(CreateCredentialsRequestValidationResource.CredentialsExist)
      .MustAsync(async (r, _) => !await credentialsRepository.DoesLoginExistAsync(r.Login))
      .WithMessage(CreateCredentialsRequestValidationResource.LoginExist)
      .MustAsync(async (r, _) =>
        (await repository.GetAsync(r.UserId))?.Password == r.Password)
      .WithMessage(CreateCredentialsRequestValidationResource.PasswordWrong);
  }
}
