using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Gender;

namespace UniversityHelper.UserService.Mappers.Db.Interfaces;

[AutoInject]
public interface IDbGenderMapper
{
  DbGender Map(CreateGenderRequest request);
}
