using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto.Requests.Avatar;

namespace UniversityHelper.UserService.Validation.Image.Interfaces;

[AutoInject]
public interface IRemoveAvatarsRequestValidator : IValidator<RemoveAvatarsRequest>
{
}
