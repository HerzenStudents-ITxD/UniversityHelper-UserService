using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Pending.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Requests.PendingUser.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class PendingController : ControllerBase
  {
    [HttpGet("check")]
    public async Task<OperationResultResponse<bool>> СheckAsync(
      [FromServices] ICheckPendingUserCommand command,
      [FromQuery] Guid userId)
    {
      return await command.ExecuteAsync(userId);
    }

    [HttpGet("resendinvitation")]
    public async Task<OperationResultResponse<bool>> ResendInvitationAsync(
      [FromServices] IResendInvitationCommand command,
      [FromQuery] Guid userId,
      [FromQuery] Guid communicationId)
    {
      return await command.ExecuteAsync(userId, communicationId);
    }

    [HttpDelete("remove")]
    public async Task<OperationResultResponse<bool>> RemoveAsync(
      [FromServices] IRemovePendingUserCommand command,
      [FromQuery] Guid userId)
    {
      return await command.ExecuteAsync(userId);
    }
  }
}
