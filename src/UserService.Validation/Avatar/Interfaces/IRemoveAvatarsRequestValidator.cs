using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto.Requests.Avatar;

namespace HerzenHelper.UserService.Validation.Image.Interfaces
{
  [AutoInject]
  public interface IRemoveAvatarsRequestValidator : IValidator<RemoveAvatarsRequest>
  {
  }
}
