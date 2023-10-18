using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto.Requests.Communication;

namespace HerzenHelper.UserService.Validation.Communication.Interfaces
{
  [AutoInject]
  public interface ICreateCommunicationRequestValidator : IValidator<CreateCommunicationRequest>
  {
  }
}
