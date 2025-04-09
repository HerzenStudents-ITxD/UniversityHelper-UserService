using UniversityHelper.Core.FluentValidationExtensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Password.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Mappers.Helpers.Password;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials.Filters;
using UniversityHelper.UserService.Validation.Password.Interfaces;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Password;

public class ReconstructPasswordCommand : IReconstructPasswordCommand
{
  private readonly IReconstructPassordRequestValidator _validator;
  private readonly IUserCredentialsRepository _repository;
  private readonly IResponseCreator _responseCreator;

  public ReconstructPasswordCommand(
    IReconstructPassordRequestValidator validator,
    IUserCredentialsRepository repository,
    IResponseCreator responseCreator)
  {
    _validator = validator;
    _repository = repository;
    _responseCreator = responseCreator;
  }

  public async Task<OperationResultResponse<bool>> ExecuteAsync(ReconstructPasswordRequest request)
  {
    if (!_validator.ValidateCustom(request, out List<string> errors))
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
    }

    DbUserCredentials dbUserCredentials = await _repository.GetAsync(new GetCredentialsFilter() { UserId = request.UserId });

    if (dbUserCredentials is null)
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
    }

    dbUserCredentials.Salt = $"{Guid.NewGuid()}{Guid.NewGuid()}";
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
