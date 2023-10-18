using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.FluentValidationExtensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Password.Interfaces;
using HerzenHelper.UserService.Business.Commands.Password.Resources;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Helpers.Password;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Credentials.Filters;
using HerzenHelper.UserService.Models.Dto.Requests.Password;
using HerzenHelper.UserService.Validation.Password.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Password
{
  public class ChangePasswordCommand : IChangePasswordCommand
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPasswordValidator _validator;
    private readonly IUserCredentialsRepository _repository;
    private readonly IResponseCreator _responseCreator;

    public ChangePasswordCommand(
      IHttpContextAccessor httpContextAccessor,
      IPasswordValidator validator,
      IUserCredentialsRepository repository,
      IResponseCreator responseCreator)
    {
      _httpContextAccessor = httpContextAccessor;
      _validator = validator;
      _repository = repository;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(ChangePasswordRequest request)
    {
      Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

      if (!_validator.ValidateCustom(request.NewPassword, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      DbUserCredentials dbUserCredentials = await _repository
        .GetAsync(new GetCredentialsFilter() { UserId = _httpContextAccessor.HttpContext.GetUserId() });

      if (dbUserCredentials is null)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
      }

      if (dbUserCredentials.PasswordHash
        != UserPasswordHash.GetPasswordHash(dbUserCredentials.Login, dbUserCredentials.Salt, request.Password))
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest, new List<string>() { ChangePasswordCommandResource.WrongOldPassword });
      }

      dbUserCredentials.PasswordHash = UserPasswordHash.GetPasswordHash(
        dbUserCredentials.Login,
        dbUserCredentials.Salt,
        request.NewPassword);

      OperationResultResponse<bool> response = new();

      response.Body = await _repository.EditAsync(dbUserCredentials);

      return response.Body
        ? response
        : _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
    }
  }
}
