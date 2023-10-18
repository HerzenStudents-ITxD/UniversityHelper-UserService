using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Pending.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Credentials
{
  public class CheckPendingUserCommand : ICheckPendingUserCommand
  {
    private readonly IPendingUserRepository _repository;

    public CheckPendingUserCommand(IPendingUserRepository repository)
    {
      _repository = repository;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId)
    {
      return new OperationResultResponse<bool>(
        body: await _repository.DoesExistAsync(userId));
    }
  }
}
