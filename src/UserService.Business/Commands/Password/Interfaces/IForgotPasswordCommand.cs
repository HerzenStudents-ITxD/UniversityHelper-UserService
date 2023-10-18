using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Password.Interfaces
{
  [AutoInject]
  public interface IForgotPasswordCommand
  {
    Task<OperationResultResponse<string>> ExecuteAsync(string userEmail);
  }
}
