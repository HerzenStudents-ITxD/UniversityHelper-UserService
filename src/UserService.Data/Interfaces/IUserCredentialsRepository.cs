using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials.Filters;

namespace UniversityHelper.UserService.Data.Interfaces;

[AutoInject]
public interface IUserCredentialsRepository
{
  Task<DbUserCredentials> GetAsync(GetCredentialsFilter filter);

  Task<Guid?> CreateAsync(DbUserCredentials dbUserCredentials);

  Task<bool> EditAsync(DbUserCredentials userCredentials);

  Task<bool> DoesLoginExistAsync(string login);

  Task<bool> DoesExistAsync(Guid userId);
}
