using FluentValidation;
using HerzenHelper.UserService.Models.Dto;
using HerzenHelper.UserService.Validation.Password.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace HerzenHelper.UserService.Validation.Password
{
  public class ReconstructPasswordRequestValidator : AbstractValidator<ReconstructPasswordRequest>, IReconstructPassordRequestValidator
  {
    public ReconstructPasswordRequestValidator(
      IMemoryCache cache,
      IPasswordValidator passwordValidator)
    {
      RuleFor(r => r.UserId)
        .NotEmpty().WithMessage("User id must not be empty.");

      RuleFor(r => r.NewPassword)
        .SetValidator(passwordValidator);

      RuleFor(r => r)
        .Must(r => cache.TryGetValue(r.Secret, out Guid savedUserId) && savedUserId == r.UserId)
        .WithMessage("Invalid user data.");
    }
  }
}
