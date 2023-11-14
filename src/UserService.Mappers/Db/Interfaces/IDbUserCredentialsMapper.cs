using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;
using System;

namespace UniversityHelper.UserService.Mappers.Db.Interfaces;

[AutoInject]
public interface IDbUserCredentialsMapper
{
  DbUserCredentials Map(CreateCredentialsRequest request);

  DbUserCredentials Map(
    Guid userId,
    string login,
    string password);
}
