using FluentValidation;
using HerzenHelper.Core.Validators.Interfaces;
using HerzenHelper.UserService.Models.Dto.Requests.Avatar;
using HerzenHelper.UserService.Validation.Image.Interfaces;

namespace HerzenHelper.UserService.Validation.Avatars
{
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
}
