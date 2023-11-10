using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Gender.Interfaces;
using UniversityHelper.UserService.Business.Commands.User.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.Gender;
using UniversityHelper.UserService.Models.Dto.Requests.Gender.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UniversityHelper.UserService.Controllers
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
