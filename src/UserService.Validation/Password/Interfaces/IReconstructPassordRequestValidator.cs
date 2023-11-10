using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto;

namespace UniversityHelper.UserService.Validation.Password.Interfaces
{
  [AutoInject]
  public interface IReconstructPassordRequestValidator : IValidator<ReconstructPasswordRequest>
  {
  }
}
