using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto.Requests.User;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.UserService.Validation.User.Interfaces
{
  [AutoInject]
  public interface IEditUserRequestValidator : IValidator<JsonPatchDocument<EditUserRequest>>
  {
  }
}
