using FluentValidation;
using UniversityHelper.Core.Validators.Interfaces;
using UniversityHelper.UserService.Models.Dto.Requests.Avatar;
using UniversityHelper.UserService.Validation.Image.Interfaces;

namespace UniversityHelper.UserService.Validation.Avatars;

public class CreateAvatarRequestValidator : AbstractValidator<CreateAvatarRequest>, ICreateAvatarRequestValidator
{
  public CreateAvatarRequestValidator(
    IImageContentValidator imageContentValidator,
    IImageExtensionValidator imageExtensionValidator)
  {
    RuleFor(x => x.Content)
      .SetValidator(imageContentValidator);

    RuleFor(x => x.Extension)
      .SetValidator(imageExtensionValidator);
  }
}
