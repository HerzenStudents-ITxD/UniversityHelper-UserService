using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Requests.Gender.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Gender.Interfaces
{
  [AutoInject]
  public interface IFindGenderCommand
  {
    Task<FindResultResponse<GenderInfo>> ExecuteAsync(FindGendersFilter filter);
  }
}
