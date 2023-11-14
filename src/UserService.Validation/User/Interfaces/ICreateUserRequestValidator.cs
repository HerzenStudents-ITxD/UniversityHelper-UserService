using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto;

namespace UniversityHelper.UserService.Validation.User.Interfaces;

  [AutoInject]
  public interface ICreateUserRequestValidator : IValidator<CreateUserRequest>
  {
  }
