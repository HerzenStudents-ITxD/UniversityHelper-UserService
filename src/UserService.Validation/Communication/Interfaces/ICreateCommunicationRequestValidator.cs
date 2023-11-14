using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;

namespace UniversityHelper.UserService.Validation.Communication.Interfaces;

[AutoInject]
public interface ICreateCommunicationRequestValidator : IValidator<CreateCommunicationRequest>
{
}
