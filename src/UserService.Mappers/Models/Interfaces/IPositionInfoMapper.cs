using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models.Position;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
    public interface IPositionInfoMapper
    {
        PositionInfo Map(PositionData position);
    }
}
