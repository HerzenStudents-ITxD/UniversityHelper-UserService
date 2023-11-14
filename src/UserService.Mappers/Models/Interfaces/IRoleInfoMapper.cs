using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.Right;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces;

[AutoInject]
public interface IRoleInfoMapper
{
  RoleInfo Map(RoleData role);
}
