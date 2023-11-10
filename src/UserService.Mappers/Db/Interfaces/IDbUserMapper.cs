using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Requests.User;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto;

namespace UniversityHelper.UserService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserMapper
  {
    DbUser Map(CreateUserRequest request);

    DbUser Map(ICreateAdminRequest request);
  }
}
