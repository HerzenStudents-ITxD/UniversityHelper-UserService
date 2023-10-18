using FluentValidation;
using HerzenHelper.Core.Attributes;

namespace HerzenHelper.UserService.Validation.Password.Interfaces
{
  [AutoInject]
  public interface IPasswordValidator : IValidator<string>
  {
  }
}
