using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models.Office;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
    public interface IOfficeInfoMapper
    {
        OfficeInfo Map(OfficeData office);
    }
}
