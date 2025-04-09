using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.UserService.Business.Commands.User.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using Microsoft.AspNetCore.Http;

namespace UniversityHelper.UserService.Business.Commands.User;

public class GetUserInfoCommand : IGetUserInfoCommand
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IUserRepository _repository;

  public GetUserInfoCommand(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository repository)
  {
    _httpContextAccessor = httpContextAccessor;
    _repository = repository;
  }

  public async Task<OperationResultResponse<UserData>> ExecuteAsync()
  {
    DbUser dbUser = await _repository.GetAsync(_httpContextAccessor.HttpContext.GetUserId());
    return new OperationResultResponse<UserData>(body: new UserData(
        id: dbUser.Id,
        imageId: null,
        firstName: dbUser.FirstName,
        middleName: dbUser.MiddleName,
        lastName: dbUser.LastName,
        isActive: dbUser.IsActive));
  }
}
