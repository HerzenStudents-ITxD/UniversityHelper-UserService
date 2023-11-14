using FluentValidation;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.UserService.Validation.Password.Interfaces;

[AutoInject]
public interface IPasswordValidator : IValidator<string>
{
}
