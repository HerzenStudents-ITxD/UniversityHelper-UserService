using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UniversityHelper.UserService.Models.Db;

public class DbUserAddition
{
  public const string TableName = "UsersAdditions";

  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string About { get; set; }
  public DateTime? DateOfBirth { get; set; }

  public Guid ModifiedBy { get; set; }
  public DateTime ModifiedAtUtc { get; set; }
  public DbUser User { get; set; }

}

public class DbUserAdditionConfiguration : IEntityTypeConfiguration<DbUserAddition>
{
  public void Configure(EntityTypeBuilder<DbUserAddition> builder)
  { 
    builder
      .ToTable(DbUserAddition.TableName);

    builder
      .HasKey(x => x.Id);

    builder
      .HasOne(ua => ua.User)
      .WithOne(u => u.Addition);

  }
}
