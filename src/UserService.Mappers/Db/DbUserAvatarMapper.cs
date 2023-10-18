using HerzenHelper.UserService.Mappers.Db.Interfaces;
using HerzenHelper.UserService.Models.Db;
using System;

namespace HerzenHelper.UserService.Mappers.Db
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
