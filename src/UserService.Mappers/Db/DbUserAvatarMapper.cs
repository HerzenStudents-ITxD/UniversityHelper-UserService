using UniversityHelper.UserService.Mappers.Db.Interfaces;
using UniversityHelper.UserService.Models.Db;
using System;

namespace UniversityHelper.UserService.Mappers.Db
{
  public class DbUserAvatarMapper : IDbUserAvatarMapper
  {
    public DbUserAvatar Map(
      Guid avatarId,
      Guid userId,
      bool isCurrentAvatar = false)
    {
      return new DbUserAvatar
      {
        Id = Guid.NewGuid(),
        UserId = userId,
        AvatarId = avatarId,
        IsCurrentAvatar = isCurrentAvatar
      };
    }
  }
}
