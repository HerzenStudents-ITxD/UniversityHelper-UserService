using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Pending.Interfaces
{
  [AutoInject]
  public interface IResendInvitationCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, Guid communicationId);
  }
}
