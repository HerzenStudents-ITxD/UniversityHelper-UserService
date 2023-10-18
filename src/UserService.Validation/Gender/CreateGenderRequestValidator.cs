using FluentValidation;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Models.Dto.Requests.Gender;
using HerzenHelper.UserService.Validation.Gender.Interfaces;
using HerzenHelper.UserService.Validation.Gender.Resources;
using System.Globalization;
using System.Threading;

namespace HerzenHelper.UserService.Validation.Gender
{
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
}
