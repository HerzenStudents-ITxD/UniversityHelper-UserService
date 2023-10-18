using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Requests.User;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto;

namespace HerzenHelper.UserService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserMapper
  {
    DbUser Map(CreateUserRequest request);

    DbUser Map(ICreateAdminRequest request);
  }
}
