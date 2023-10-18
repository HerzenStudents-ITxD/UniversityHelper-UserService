using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Credentials.Filters;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Data.Interfaces
{
  [AutoInject]
  public interface IUserCredentialsRepository
  {
    Task<DbUserCredentials> GetAsync(GetCredentialsFilter filter);

    Task<Guid?> CreateAsync(DbUserCredentials dbUserCredentials);

    Task<bool> EditAsync(DbUserCredentials userCredentials);

    Task<bool> DoesLoginExistAsync(string login);

    Task<bool> DoesExistAsync(Guid userId);
  }
}
