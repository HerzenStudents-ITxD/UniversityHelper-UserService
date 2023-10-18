using HerzenHelper.Models.Broker.Models.Position;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models
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
