using FluentValidation;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using UniversityHelper.UserService.Validation.Communication.Interfaces;
using UniversityHelper.UserService.Validation.Communication.Resources;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace UniversityHelper.UserService.Validation.Communication;

public class CreateCommunicationRequestValidator : AbstractValidator<CreateCommunicationRequest>, ICreateCommunicationRequestValidator
{
  private static Regex PhoneRegex = new(@"^\d+$");

  public CreateCommunicationRequestValidator(
    IUserCommunicationRepository _communicationRepository)
  {
    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

    RuleFor(c => c.Type)
      .IsInEnum()
      .WithMessage(CreateCommunicationRequestValidatorResource.IncorrectCommunicationTypeFormat)
      .Must(ct => ct != CommunicationType.BaseEmail)
      .WithMessage(CreateCommunicationRequestValidatorResource.UnconfirmedEmail);
     
    When(c => c.Type == CommunicationType.Phone, () =>
      RuleFor(c => c.Value)
        .Must(v => PhoneRegex.IsMatch(v.Trim())).WithMessage(CreateCommunicationRequestValidatorResource.IncorrectPhoneNumber));

    When(c => c.Type == CommunicationType.Email, () =>
      RuleFor(c => c.Value)
        .Must(v =>
        {
          try
          {
            MailAddress address = new(v.Trim());
            return true;
          }
          catch
          {
            return false;
          }
        })
        .WithMessage(CreateCommunicationRequestValidatorResource.IncorrectEmailAddress));

    RuleFor(c => c.Value)
      .MustAsync(async (v, _, _) => !await _communicationRepository.DoesValueExist(v.Value))
      .WithMessage(CreateCommunicationRequestValidatorResource.ExistingCommunicationValue);
  }
}
