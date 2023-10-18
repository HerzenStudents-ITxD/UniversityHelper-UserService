using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Credentials;
using System;

namespace HerzenHelper.UserService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserCredentialsMapper
  {
    DbUserCredentials Map(CreateCredentialsRequest request);

    DbUserCredentials Map(
      Guid userId,
      string login,
      string password);
  }
}
