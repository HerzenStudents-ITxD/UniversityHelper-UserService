using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Gender.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Data.Interfaces
{
  [AutoInject]
  public interface IGenderRepository
  {
    Task CreateAsync(DbGender gender);

    Task<bool> DoesGenderAlreadyExistAsync(string genderName);

    Task<(List<DbGender> dbGenders, int totalCount)> FindGendersAsync(FindGendersFilter filter);
  }
}
