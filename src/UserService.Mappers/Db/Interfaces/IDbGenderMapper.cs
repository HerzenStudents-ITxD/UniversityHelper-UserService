using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Gender;

namespace HerzenHelper.UserService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbGenderMapper
  {
    DbGender Map(CreateGenderRequest request);
  }
}
