using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.EFSupport.Provider;
using HerzenHelper.Core.Enums;
using HerzenHelper.UserService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace HerzenHelper.CompanyService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbUser> Users { get; set; }
    DbSet<DbUserAddition> UsersAdditions { get; set; }
    DbSet<DbUserCredentials> UsersCredentials { get; set; }
    DbSet<DbUserCommunication> UsersCommunications { get; set; }
    DbSet<DbPendingUser> PendingUsers { get; set; }
    DbSet<DbUserAvatar> UsersAvatars { get; set; }
    DbSet<DbGender> Genders { get; set; }
  }
}
