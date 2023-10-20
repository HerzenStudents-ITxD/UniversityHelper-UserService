using HerzenHelper.UserService.Business.Commands.Credentials.Interfaces;
using HerzenHelper.UserService.Models.Dto.Requests.Credentials;
using HerzenHelper.UserService.Models.Dto.Responses.Credentials;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HerzenHelper.Core.Responses;

namespace HerzenHelper.UserService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CredentialsController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<CredentialsResponse>> CreateAsync(
      [FromServices] ICreateCredentialsCommand command,
      [FromBody] CreateCredentialsRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPut("reactivate")]
    public async Task<OperationResultResponse<CredentialsResponse>> ReactivateAcync(
      [FromServices] IReactivateCredentialsCommand command,
      [FromBody] ReactivateCredentialsRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
