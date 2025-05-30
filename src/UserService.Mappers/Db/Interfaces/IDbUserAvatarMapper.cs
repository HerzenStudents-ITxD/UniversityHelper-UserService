﻿using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;

namespace UniversityHelper.UserService.Mappers.Db.Interfaces;

[AutoInject]
public interface IDbUserAvatarMapper
{
  DbUserAvatar Map(
    Guid avatarId,
    Guid userId,
    bool isCurrentAvatar = false);
}
