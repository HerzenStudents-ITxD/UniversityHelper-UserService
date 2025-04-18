﻿using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models;

public class UserInfoMapper : IUserInfoMapper
{
  public UserInfo Map(
    DbUser dbUser,
    ImageInfo avatar)
  {
    return dbUser is null ? default : new UserInfo
    {
      Id = dbUser.Id,
      FirstName = dbUser.FirstName,
      LastName = dbUser.LastName,
      MiddleName = dbUser.MiddleName,
      IsAdmin = dbUser.IsAdmin,
      IsActive = dbUser.IsActive,
      PendingInfo = dbUser.Pending is null ? null : new PendingUserInfo()
      { InvitationCommunicationId = dbUser.Pending.CommunicationId },
      Avatar = avatar,
      Communications = dbUser.Communications
        ?.Select(c => new CommunicationInfo
        {
          Id = c.Id,
          Type = (CommunicationType)c.Type,
          Value = c.Value,
          IsConfirmed = c.IsConfirmed
        }),
    };
  }
}
