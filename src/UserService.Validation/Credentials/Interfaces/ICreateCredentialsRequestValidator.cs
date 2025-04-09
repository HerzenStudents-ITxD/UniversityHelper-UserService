using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;

namespace UniversityHelper.UserService.Validation.Credentials.Interfaces;

  [AutoInject]
  public interface ICreateCredentialsRequestValidator : IValidator<CreateCredentialsRequest>
  {
  }
