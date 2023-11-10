using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.UserService.Models.Dto.Models;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.User.Interfaces
{
  [AutoInject]
  public interface IGetUserInfoCommand
  {
    Task<OperationResultResponse<UserData>> ExecuteAsync();
  }
}
