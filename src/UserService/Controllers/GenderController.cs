using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Gender.Interfaces;
using HerzenHelper.UserService.Business.Commands.User.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Requests.Gender;
using HerzenHelper.UserService.Models.Dto.Requests.Gender.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HerzenHelper.UserService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class GenderController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateGenderCommand command,
      [FromBody] CreateGenderRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<GenderInfo>> FindAsync(
      [FromServices] IFindGenderCommand command,
      [FromQuery] FindGendersFilter request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
