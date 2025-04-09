using UniversityHelper.UniversityService.Data.Provider;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.UserService.Data;

public class PendingUserRepository : IPendingUserRepository
{
  private readonly IDataProvider _provider;

  public PendingUserRepository(
    IDataProvider provider)
  {
    _provider = provider;
  }

  public Task CreateAsync(DbPendingUser dbPendingUser)
  {
    _provider.PendingUsers.Add(dbPendingUser);
    return _provider.SaveAsync();
  }

  public Task<DbPendingUser> GetAsync(Guid userId, bool includeUser)
  {
    IQueryable<DbPendingUser> query = _provider.PendingUsers.AsQueryable();

    if (includeUser)
    {
      query = query.Include(pu => pu.User).ThenInclude(u => u.Communications);
    }

    return query.FirstOrDefaultAsync(pu => pu.UserId == userId);
  }

  public Task UpdateAsync(DbPendingUser dbPendingUser)
  {
    _provider.PendingUsers.Update(dbPendingUser);
    return _provider.SaveAsync();
  }

  public async Task<DbPendingUser> RemoveAsync(Guid userId)
  {
    DbPendingUser dbPendingUser = await _provider.PendingUsers
      .FirstOrDefaultAsync(pu => pu.UserId == userId);

    if (dbPendingUser is not null)
    {
      _provider.PendingUsers.Remove(dbPendingUser);
      await _provider.SaveAsync();
    }

    return dbPendingUser;
  }

  public Task<bool> DoesExistAsync(Guid userId)
  {
    return _provider.PendingUsers.AnyAsync(pu => pu.UserId == userId);
  }
}
