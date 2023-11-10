using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Position;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
    public interface IPositionInfoMapper
    {
        PositionInfo Map(PositionData position);
    }
}
