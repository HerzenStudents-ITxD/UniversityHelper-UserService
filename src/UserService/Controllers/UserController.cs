using HerzenHelper.Core.Responses;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.UserService.Business.Commands.User.Interfaces;
using HerzenHelper.UserService.Business.Interfaces;
using HerzenHelper.UserService.Models.Dto;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Requests.Filtres;
using HerzenHelper.UserService.Models.Dto.Requests.User;
using HerzenHelper.UserService.Models.Dto.Requests.User.Filters;
using HerzenHelper.UserService.Models.Dto.Responses.User;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid>> CreateAsync(
      [FromServices] ICreateUserCommand command,
      [FromBody] CreateUserRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditUserCommand command,
      [FromQuery] Guid userId,
      [FromBody] JsonPatchDocument<EditUserRequest> request)
    {
      return await command.ExecuteAsync(userId, request);
    }

    [HttpGet("get")]
    public async Task<OperationResultResponse<UserResponse>> GetAsync(
      [FromServices] IGetUserCommand command,
      [FromQuery] GetUserFilter filter,
      CancellationToken cansellationToken)
    {
      return await command.ExecuteAsync(filter, cansellationToken);
    }

    [HttpGet("getinfo")]
    public async Task<OperationResultResponse<UserData>> GetInfoAsync(
      [FromServices] IGetUserInfoCommand command)
    {
      return await command.ExecuteAsync();
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<UserInfo>> FindAsync(
      [FromServices] IFindUserCommand command,
      [FromQuery] FindUsersFilter filter,
      CancellationToken cansellationToken)
    {
      return await command.ExecuteAsync(filter, cansellationToken);
    }

    [HttpPut("editactive")]
    public async Task<OperationResultResponse<bool>> EditStatusAsync(
      [FromServices] IEditUserActiveCommand command,
      [FromBody] EditUserActiveRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
