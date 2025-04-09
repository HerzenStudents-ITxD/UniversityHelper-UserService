using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Pending.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;

namespace UniversityHelper.UserService.Business.Commands.Credentials;

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
