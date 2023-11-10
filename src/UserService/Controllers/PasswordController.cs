using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Password.Interfaces;
using UniversityHelper.UserService.Models.Dto;
using UniversityHelper.UserService.Models.Dto.Requests.Password;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Controllers
{
  [ApiController]
  [Route("[controller]")]
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
