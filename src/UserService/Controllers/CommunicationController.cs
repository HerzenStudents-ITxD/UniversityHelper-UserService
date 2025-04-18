﻿using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Communication.Interfaces;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunicationController : ControllerBase
{
  [HttpPost("create")]
  public async Task<OperationResultResponse<Guid?>> CreateAsync(
    [FromServices] ICreateCommunicationCommand command,
    [FromBody] CreateCommunicationRequest request)
  {
    return await command.ExecuteAsync(request);
  }

  [HttpPut("edit")]
  public async Task<OperationResultResponse<bool>> EditAsync(
    [FromServices] IEditCommunicationCommand command,
    [FromBody] EditCommunicationRequest request,
    [FromQuery] Guid communicationId)
  {
    return await command.ExecuteAsync(communicationId, request);
  }

  [HttpDelete("remove")]
  public async Task<OperationResultResponse<bool>> RemoveAsync(
    [FromServices] IRemoveCommunicationCommand command,
    [FromQuery] Guid communicationId)
  {
    return await command.ExecuteAsync(communicationId);
  }

  [HttpPut("confirm")]
  public async Task<OperationResultResponse<bool>> ConfirmAsync(
    [FromServices] IConfirmCommunicationCommand command,
    [FromQuery] Guid communicationId,
    [FromQuery] string secret)
  {
    return await command.ExecuteAsync(communicationId, secret);
  }

  [HttpGet("resendconfirmation")]
  public async Task<OperationResultResponse<bool>> ConfirmAsync(
    [FromServices] IResendConfirmationCommunicationCommand command,
    [FromQuery] Guid communicationId)
  {
    return await command.ExecuteAsync(communicationId);
  }
}
