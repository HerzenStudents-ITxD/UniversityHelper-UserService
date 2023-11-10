using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto.Requests.Gender;

namespace UniversityHelper.UserService.Validation.Gender.Interfaces
{
  [AutoInject]
  public interface ICreateGenderRequestValidator : IValidator<CreateGenderRequest>
  {
  }
}
