using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("UniversityHelper.UserService.Business")]
[assembly: InternalsVisibleTo("UniversityHelper.UserService.Business.UnitTests")]
[assembly: InternalsVisibleTo("UniversityHelper.UserService.Mappers.UnitTests")]

namespace UniversityHelper.UserService.Mappers.Helpers.Password;

internal static class UserPasswordHash
{
  private const string INTERNAL_SALT = "UniversityHelper.SALT3";

  internal static string GetPasswordHash(string userLogin, string salt, string userPassword)
  {
    return Encoding.UTF8.GetString(SHA512.Create().ComputeHash(
      Encoding.UTF8.GetBytes($"{salt}{userLogin}{userPassword}{INTERNAL_SALT}")));
  }
}