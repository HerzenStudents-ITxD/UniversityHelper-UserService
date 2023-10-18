using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Data.Interfaces
{
  [AutoInject]
  public interface IPendingUserRepository
  {
    Task CreateAsync(DbPendingUser dbPendingUser);

    Task<DbPendingUser> GetAsync(Guid userId, bool includeUser = false);

    Task UpdateAsync(DbPendingUser dbPendingUser);

    Task<DbPendingUser> RemoveAsync(Guid userId);

    Task<bool> DoesExistAsync(Guid userId);
  }
}
