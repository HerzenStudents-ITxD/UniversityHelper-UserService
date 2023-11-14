using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Responses.User;
using System.Collections.Generic;

namespace UniversityHelper.UserService.Mappers.Responses.Interfaces;

[AutoInject]
public interface IUserResponseMapper
{
  UserResponse Map(
    DbUser dbUser,
    CompanyUserInfo companyUser,
    ImageInfo avatar,
    DepartmentUserInfo departmentUser,
    List<ImageInfo> images,
    OfficeInfo office,
    PositionInfo position,
    RoleInfo role);
}
