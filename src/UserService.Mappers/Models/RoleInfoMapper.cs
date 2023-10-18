using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.Right;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models
{
  public class RoleInfoMapper : IRoleInfoMapper
  {
    public RoleInfo Map(RoleData role)
    {
      return role is null
        ? default
        : new RoleInfo
        {
          Id = role.Id,
          Name = role.Name,
          //RightsIds = role.RightsIds
        };
    }
  }
}
