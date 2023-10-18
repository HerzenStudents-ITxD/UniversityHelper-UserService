using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.Right;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IRoleInfoMapper
  {
    RoleInfo Map(RoleData role);
  }
}
