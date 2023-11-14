using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.Filtres;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Interfaces;

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
