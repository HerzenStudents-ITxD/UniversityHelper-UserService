using FluentValidation;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Dto.Requests.Gender;
using UniversityHelper.UserService.Validation.Gender.Interfaces;
using UniversityHelper.UserService.Validation.Gender.Resources;
using System.Globalization;
using System.Threading;

namespace UniversityHelper.UserService.Validation.Gender;

public class CreateGenderRequestValidator : AbstractValidator<CreateGenderRequest>, ICreateGenderRequestValidator
{
  public CreateGenderRequestValidator(IGenderRepository genderRepository)
  {
    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

    RuleFor(gender => gender.Name)
      .MustAsync(async (name, _) => !await genderRepository.DoesGenderAlreadyExistAsync(name))
      .WithMessage(CreateGenderRequestValidationResource.NameExists);
  }
}
