using UniversityHelper.Models.Broker.Models.Right;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models;

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
