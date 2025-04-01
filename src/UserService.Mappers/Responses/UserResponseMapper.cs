using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Mappers.Responses.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Responses.User;
using System.Collections.Generic;

namespace UniversityHelper.UserService.Mappers.Responses;

public class UserResponseMapper : IUserResponseMapper
{
  private readonly IUserInfoMapper _userInfoMapper;

  public UserResponseMapper(
    IUserInfoMapper userInfoMapper)
  {
    _userInfoMapper = userInfoMapper;
  }

  public UserResponse Map(
    DbUser dbUser,
    CompanyUserInfo companyUser,
    ImageInfo avatar,
    DepartmentUserInfo departmentUser,
    List<ImageInfo> images,
    OfficeInfo office,
    PositionInfo position,
    RoleInfo role)
  {
    return dbUser is null
      ? default
      : new UserResponse
      {
        User = _userInfoMapper.Map(dbUser, avatar),
        UserAddition = dbUser.Addition is null ? null : new()
        {
          About = dbUser.Addition.About,
          DateOfBirth = dbUser.Addition.DateOfBirth
        },
        CompanyUser = companyUser,
        DepartmentUser = departmentUser,
        Office = office,
        Position = position,
        Role = role,
        Images = images
      };
  }
}
