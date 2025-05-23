﻿using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Pending.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.UserService.Controllers;

[ApiController]
[Route("[controller]")]
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
