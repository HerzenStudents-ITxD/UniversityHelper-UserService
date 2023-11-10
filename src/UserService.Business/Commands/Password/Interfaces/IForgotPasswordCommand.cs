using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Password.Interfaces
{
  [AutoInject]
  public interface IForgotPasswordCommand
  {
    Task<OperationResultResponse<string>> ExecuteAsync(string userEmail);
  }
}
