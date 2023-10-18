using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("HerzenHelper.UserService.Business")]
[assembly: InternalsVisibleTo("HerzenHelper.UserService.Business.UnitTests")]
[assembly: InternalsVisibleTo("HerzenHelper.UserService.Mappers.UnitTests")]

namespace HerzenHelper.UserService.Mappers.Helpers.Password
{
  internal static class UserPasswordHash
  {
    private const string INTERNAL_SALT = "HerzenHelper.SALT3";

    internal static string GetPasswordHash(string userLogin, string salt, string userPassword)
    {
      return Encoding.UTF8.GetString(SHA512.Create().ComputeHash(
        Encoding.UTF8.GetBytes($"{salt}{userLogin}{userPassword}{INTERNAL_SALT}")));
    }
  }
}