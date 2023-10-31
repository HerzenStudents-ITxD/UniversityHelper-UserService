using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Responses;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.User;
using HerzenHelper.UserService.Business.Commands.User.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Models.Db;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User
{
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
}
