using UniversityHelper.UniversityService.Data.Provider;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.UserService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UniversityHelper.UserService.Data.Provider.MsSql.Ef;

public class UserServiceDbContext : DbContext, IDataProvider
{
  public DbSet<DbUser> Users { get; set; }
  public DbSet<DbUserAddition> UsersAdditions { get; set; }
  public DbSet<DbUserCredentials> UsersCredentials { get; set; }
  public DbSet<DbUserCommunication> UsersCommunications { get; set; }
  public DbSet<DbPendingUser> PendingUsers { get; set; }
  public DbSet<DbUserAvatar> UsersAvatars { get; set; }


  public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
    : base(options)
  {
  }

  // Fluent API is written here.
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("UniversityHelper.UserService.Models.Db"));
  }

  public object MakeEntityDetached(object obj)
  {
    Entry(obj).State = EntityState.Detached;
    return Entry(obj).State;
  }

  async Task IBaseDataProvider.SaveAsync()
  {
    await SaveChangesAsync();
  }

  void IBaseDataProvider.Save()
  {
    SaveChanges();
  }

  public void EnsureDeleted()
  {
    Database.EnsureDeleted();
  }

  public bool IsInMemory()
  {
    return Database.IsInMemory();
  }
}
