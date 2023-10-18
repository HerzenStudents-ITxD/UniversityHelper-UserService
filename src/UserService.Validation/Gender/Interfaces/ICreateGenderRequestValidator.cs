using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto.Requests.Gender;

namespace HerzenHelper.UserService.Validation.Gender.Interfaces
{
  [AutoInject]
  public interface ICreateGenderRequestValidator : IValidator<CreateGenderRequest>
  {
  }
}
