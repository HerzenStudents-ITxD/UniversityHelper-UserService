using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.University;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IUniversityService
{
  Task<List<UniversityData>> GetUniversitiesAsync(Guid userId, List<string> errors, CancellationToken cancellationToken = default);
}
