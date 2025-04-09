using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Right;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IRightService
{
  Task<List<RoleData>> GetRolesAsync(
    Guid userId,
    string locale,
    List<string> errors,
    CancellationToken cancellationToken = default);
}
