using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.Gender.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Gender.Interfaces
{
  [AutoInject]
  public interface IFindGenderCommand
  {
    Task<FindResultResponse<GenderInfo>> ExecuteAsync(FindGendersFilter filter);
  }
}
