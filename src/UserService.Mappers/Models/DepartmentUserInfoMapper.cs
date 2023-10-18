using HerzenHelper.Models.Broker.Models.Department;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;
using System;
using System.Linq;

namespace HerzenHelper.UserService.Mappers.Models
{
  public class DepartmentUserInfoMapper : IDepartmentInfoMapper
  {
    public DepartmentUserInfo Map(Guid userId, DepartmentData department)
    {
      var user = department?.UsersIds?.FirstOrDefault(user => user == userId);

      return department is null || user is null
        ? default
        : new DepartmentUserInfo
        {
          Department = new DepartmentInfo
          {
            Id = department.Id,
            Name = department.Name,
            //ShortName = department.ShortName,
            //ChildDepartmentsIds = department.ChildDepartmentsIds
          },
          //Role = user.Role
        };
    }
  }
}
