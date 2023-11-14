using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Data.Interfaces;

[AutoInject]
public interface IUserAvatarRepository
{
  Task CreateAsync(DbUserAvatar dbEntityImage);

  Task<List<Guid>> GetAvatarsByUserId(Guid entityId, CancellationToken cancellationToken = default);

  Task<List<DbUserAvatar>> GetAsync(List<Guid> imagesIds);

  Task<bool> UpdateCurrentAvatarAsync(Guid userId, Guid imageId);

  Task<List<Guid>> RemoveAsync(Guid userId);

  Task<bool> RemoveAsync(List<Guid> imagesIds);
}
