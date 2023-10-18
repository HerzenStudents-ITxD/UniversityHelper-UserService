using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto;

namespace HerzenHelper.UserService.Validation.User.Interfaces
{
    [AutoInject]
    public interface ICreateUserRequestValidator : IValidator<CreateUserRequest>
    {
    }
}
