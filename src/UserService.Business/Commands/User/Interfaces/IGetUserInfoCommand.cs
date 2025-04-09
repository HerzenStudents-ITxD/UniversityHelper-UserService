using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Models.User;

namespace UniversityHelper.UserService.Business.Commands.User.Interfaces;

[AutoInject]
public interface IGetUserInfoCommand
{
  Task<OperationResultResponse<UserData>> ExecuteAsync();
}
