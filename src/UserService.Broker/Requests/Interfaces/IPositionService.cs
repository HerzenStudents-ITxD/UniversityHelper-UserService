using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Position;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IPositionService
{
  Task<List<PositionData>> GetPositionsAsync(Guid userId, List<string> errors, CancellationToken cancellationToken = default);
}
