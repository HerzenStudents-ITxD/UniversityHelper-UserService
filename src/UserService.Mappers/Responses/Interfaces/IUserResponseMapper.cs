using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Responses.User;
using System.Collections.Generic;

namespace HerzenHelper.UserService.Mappers.Responses.Interfaces
{
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
}
