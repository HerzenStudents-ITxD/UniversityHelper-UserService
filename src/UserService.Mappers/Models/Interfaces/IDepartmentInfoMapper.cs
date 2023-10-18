using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models.Department;
using HerzenHelper.UserService.Models.Dto.Models;
using System;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IDepartmentInfoMapper
  {
    DepartmentUserInfo Map(Guid userId, DepartmentData department);
  }
}
