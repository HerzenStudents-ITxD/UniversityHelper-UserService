using FluentValidation;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using UniversityHelper.UserService.Validation.Communication.Interfaces;
using UniversityHelper.UserService.Validation.Communication.Resources;
using System.Globalization;

namespace UniversityHelper.UserService.Validation.Communication;

public class EditCommunicationRequestValidator : AbstractValidator<(
    DbUserCommunication dbUserCommunication,
    EditCommunicationRequest request)>,
  IEditCommunicationRequestValidator
{
  private readonly IUserCommunicationRepository _communicationRepository;

  public EditCommunicationRequestValidator(
    IUserCommunicationRepository communicationRepository)
  {
    _communicationRepository = communicationRepository;

    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

    RuleFor(x => x)
      .Must(x => !(x.request.Type is not null && !string.IsNullOrEmpty(x.request.Value)))
      .WithMessage(EditCommunicationRequestValidatorResource.MoreThenOneProperty)
      .Must(x => !(x.request.Type is null && string.IsNullOrEmpty(x.request.Value)))
      .WithMessage(EditCommunicationRequestValidatorResource.NoChanges);

    When(x => x.request.Type is not null, () =>
    {
      RuleFor(x => x)
        .Cascade(CascadeMode.Stop)
        .Must(x => x.request.Type == CommunicationType.BaseEmail)
        .WithMessage(EditCommunicationRequestValidatorResource.InvalidType)
        .Must(x => x.dbUserCommunication.IsConfirmed
          && x.dbUserCommunication.Type == (int)CommunicationType.Email)
        .WithMessage(EditCommunicationRequestValidatorResource.NotVerifiedEmail);
    });

    When(x => !string.IsNullOrEmpty(x.request.Value), () =>
    {
      RuleFor(x => x.request.Value)
        .MustAsync(async (x, _) => !await _communicationRepository.DoesValueExist(x))
        .WithMessage(EditCommunicationRequestValidatorResource.ExistingCommunicationValue);
    });
  }
}
