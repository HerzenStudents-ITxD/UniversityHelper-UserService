﻿using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Avatar.Interfaces;
using HerzenHelper.UserService.Models.Dto.Requests.Avatar;
using HerzenHelper.UserService.Models.Dto.Responses.Image;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class AvatarController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateAvatarCommand command,
      [FromBody] CreateAvatarRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("get")]
    public async Task<OperationResultResponse<UserImagesResponse>> GetAsync(
      [FromServices] IGetAvatarsCommand command,
      [FromQuery] Guid userId,
      CancellationToken token)
    {
      return await command.ExecuteAsync(userId, token);
    }

    [HttpDelete("remove")]
    public async Task<OperationResultResponse<bool>> RemoveAsync(
      [FromServices] IRemoveAvatarsCommand command,
      [FromBody] RemoveAvatarsRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("editcurrent")]
    public async Task<OperationResultResponse<bool>> EditCurrentAsync(
      [FromServices] IEditAvatarCommand command,
      [FromQuery] Guid userId,
      [FromQuery] Guid avatarId)
    {
      return await command.ExecuteAsync(userId, avatarId);
    }
  }
}
