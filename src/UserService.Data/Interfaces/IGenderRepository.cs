using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Gender.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Data.Interfaces
{
  [AutoInject]
  public interface IGenderRepository
  {
    Task CreateAsync(DbGender gender);

    Task<bool> DoesGenderAlreadyExistAsync(string genderName);

    Task<(List<DbGender> dbGenders, int totalCount)> FindGendersAsync(FindGendersFilter filter);
  }
}
