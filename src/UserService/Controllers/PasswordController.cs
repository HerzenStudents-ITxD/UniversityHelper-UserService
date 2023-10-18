using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Password.Interfaces;
using HerzenHelper.UserService.Models.Dto;
using HerzenHelper.UserService.Models.Dto.Requests.Password;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class PasswordController : ControllerBase
  {
    [HttpGet("forgot")]
    public async Task<OperationResultResponse<string>> ForgotPasswordAsync(
      [FromServices] IForgotPasswordCommand command,
      [FromQuery] string userEmail)
    {
      return await command.ExecuteAsync(userEmail);
    }

    [HttpPost("reconstruct")]
    public async Task<OperationResultResponse<bool>> ReconstructPasswordAsync(
      [FromServices] IReconstructPasswordCommand command,
      [FromBody] ReconstructPasswordRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPost("change")]
    public async Task<OperationResultResponse<bool>> ChangePasswordAsync(
      [FromServices] IChangePasswordCommand command,
      [FromBody] ChangePasswordRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("generate")]
    public string GeneratePassword(
      [FromServices] IGeneratePasswordCommand command)
    {
      return command.Execute();
    }
  }
}
