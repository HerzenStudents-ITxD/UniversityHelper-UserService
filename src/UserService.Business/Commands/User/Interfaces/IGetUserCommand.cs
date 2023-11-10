using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.User.Filters;
using UniversityHelper.UserService.Models.Dto.Responses.User;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Interfaces
{
  /// <summary>
  /// Represents interface for a command in command pattern.
  /// Provides method for getting user information.
  /// </summary>
  [AutoInject]
  public interface IGetUserCommand
  {
    /// <summary>
    /// Returns the user information.
    /// </summary>
    Task<OperationResultResponse<UserResponse>> ExecuteAsync(GetUserFilter filter, CancellationToken cancellationToken = default);
  }
}
