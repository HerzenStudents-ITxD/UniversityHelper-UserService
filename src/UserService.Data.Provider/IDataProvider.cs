using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.Core.Enums;
using UniversityHelper.UserService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.CompanyService.Data.Provider;

[AutoInject(InjectType.Scoped)]
public interface IDataProvider : IBaseDataProvider
{
  DbSet<DbUser> Users { get; set; }
  DbSet<DbUserAddition> UsersAdditions { get; set; }
  DbSet<DbUserCredentials> UsersCredentials { get; set; }
  DbSet<DbUserCommunication> UsersCommunications { get; set; }
  DbSet<DbPendingUser> PendingUsers { get; set; }
  DbSet<DbUserAvatar> UsersAvatars { get; set; }
}
