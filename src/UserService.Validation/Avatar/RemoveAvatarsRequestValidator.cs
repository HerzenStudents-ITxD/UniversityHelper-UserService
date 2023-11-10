using FluentValidation;
using UniversityHelper.UserService.Models.Dto.Requests.Avatar;
using UniversityHelper.UserService.Validation.Image.Interfaces;

namespace UniversityHelper.UserService.Validation.Avatar
{
  public class RemoveAvatarsRequestValidator : AbstractValidator<RemoveAvatarsRequest>, IRemoveAvatarsRequestValidator
  {
    public RemoveAvatarsRequestValidator()
    {
      RuleFor(x => x.AvatarsIds)
        .NotEmpty().WithMessage("Images Ids can not be null.");
    }
  }
}
