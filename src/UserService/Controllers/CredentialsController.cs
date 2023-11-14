using UniversityHelper.UserService.Business.Commands.Credentials.Interfaces;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;
using UniversityHelper.UserService.Models.Dto.Responses.Credentials;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.UserService.Controllers;

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
