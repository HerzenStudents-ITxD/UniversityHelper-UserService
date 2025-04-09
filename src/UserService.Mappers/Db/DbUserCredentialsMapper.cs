using UniversityHelper.UserService.Mappers.Db.Interfaces;
using UniversityHelper.UserService.Mappers.Helpers.Password;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;

namespace UniversityHelper.UserService.Mappers.Db;

public class DbUserCredentialsMapper : IDbUserCredentialsMapper
{
  public DbUserCredentials Map(
    CreateCredentialsRequest request)
  {
    return request is null
      ? null
      : Map(request.UserId, request.Login, request.Password);
  }

  public DbUserCredentials Map(
    Guid userId,
    string login,
    string password)
  {
    string salt = $"{Guid.NewGuid()}{Guid.NewGuid()}";

    return new DbUserCredentials
    {
      Id = Guid.NewGuid(),
      UserId = userId,
      Login = login.Trim(),
      Salt = salt,
      PasswordHash = UserPasswordHash.GetPasswordHash(login, salt, password),
      IsActive = true,
      CreatedAtUtc = DateTime.UtcNow
    };
  }
}
