using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using System;

namespace HerzenHelper.UserService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserAvatarMapper
  {
    DbUserAvatar Map(
      Guid avatarId,
      Guid userId,
      bool isCurrentAvatar = false);
  }
}
