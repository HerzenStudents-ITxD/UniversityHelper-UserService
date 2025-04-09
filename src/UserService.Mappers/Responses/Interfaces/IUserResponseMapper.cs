using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Responses.User;

namespace UniversityHelper.UserService.Mappers.Responses.Interfaces;

[AutoInject]
public interface IUserResponseMapper
{
  UserResponse Map(
    DbUser dbUser,
    UniversityUserInfo universityUser,
    ImageInfo avatar,
    DepartmentUserInfo departmentUser,
    List<ImageInfo> images,
    OfficeInfo office,
    PositionInfo position,
    RoleInfo role);
}
