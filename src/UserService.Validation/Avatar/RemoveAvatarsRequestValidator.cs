using FluentValidation;
using HerzenHelper.UserService.Models.Dto.Requests.Avatar;
using HerzenHelper.UserService.Validation.Image.Interfaces;

namespace HerzenHelper.UserService.Validation.Avatar
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
