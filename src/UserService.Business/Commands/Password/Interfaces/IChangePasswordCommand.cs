using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.Password;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Password.Interfaces;

[AutoInject]
public interface IChangePasswordCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(ChangePasswordRequest request);
}
