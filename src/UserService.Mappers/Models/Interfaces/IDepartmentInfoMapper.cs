using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Department;
using UniversityHelper.UserService.Models.Dto.Models;
using System;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IDepartmentInfoMapper
  {
    DepartmentUserInfo Map(Guid userId, DepartmentData department);
  }
}
