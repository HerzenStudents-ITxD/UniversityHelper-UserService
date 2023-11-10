using UniversityHelper.Models.Broker.Models.Position;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models
{
  public class PositionInfoMapper : IPositionInfoMapper
  {
    public PositionInfo Map(PositionData position)
    {
      return position is null
        ? default
        : new PositionInfo
        {
          Id = position.Id,
          Name = position.Name
        };
    }
  }
}
