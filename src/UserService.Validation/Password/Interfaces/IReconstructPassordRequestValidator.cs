using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto;

namespace HerzenHelper.UserService.Validation.Password.Interfaces
{
  [AutoInject]
  public interface IReconstructPassordRequestValidator : IValidator<ReconstructPasswordRequest>
  {
  }
}
