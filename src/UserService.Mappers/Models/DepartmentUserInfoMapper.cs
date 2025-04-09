//using UniversityHelper.UserService.Mappers.Models.Interfaces;
//using UniversityHelper.UserService.Models.Dto.Models;

//namespace UniversityHelper.UserService.Mappers.Models;

//public class DepartmentUserInfoMapper : IDepartmentInfoMapper
//{
//  public DepartmentUserInfo Map(Guid userId, DepartmentData department)
//  {
//    var user = department?.Users?.FirstOrDefault(user => user.UserId == userId);

//    return department is null || user is null
//      ? default
//      : new DepartmentUserInfo
//      {
//        Department = new DepartmentInfo
//        {
//          Id = department.Id,
//          Name = department.Name,
//          //ShortName = department.ShortName,
//          //ChildDepartmentsIds = department.ChildDepartmentsIds
//        },
//        //Role = user.Role
//      };
//  }
//}
