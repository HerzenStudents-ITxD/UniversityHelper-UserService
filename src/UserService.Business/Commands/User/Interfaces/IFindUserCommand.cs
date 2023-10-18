using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Requests.Filtres;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Interfaces
{
  /// <summary>
  /// Represents interface for a command in command pattern.
  /// Provides method for getting list of user models with pagination and filter by full name.
  /// </summary>
  [AutoInject]
  public interface IFindUserCommand
  {
    /// <summary>
    /// Returns the list of user models using pagination and filter by full name.
    /// </summary>
    Task<FindResultResponse<UserInfo>> ExecuteAsync(FindUsersFilter filter, CancellationToken cancellationToken = default);
  }
}
