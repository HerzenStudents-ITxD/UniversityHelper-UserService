using UniversityHelper.CompanyService.Data.Provider;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Gender.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Data
{
  public class GenderRepository : IGenderRepository
  {
    private readonly IDataProvider _provider;

    public GenderRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public Task CreateAsync(DbGender gender)
    {
      _provider.Genders.Add(gender);
      return _provider.SaveAsync();
    }

    public Task<bool> DoesGenderAlreadyExistAsync(string genderName)
    {
      return _provider.Genders.AnyAsync(s => s.Name.ToLower() == genderName.ToLower());
    }

    public async Task<(List<DbGender> dbGenders, int totalCount)> FindGendersAsync(FindGendersFilter filter)
    {
      if (filter is null)
      {
        return (null, default);
      }

      IQueryable<DbGender> query = _provider.Genders.AsQueryable();

      if (!string.IsNullOrWhiteSpace(filter.NameIncludeSubstring))
      {
        query = query.Where(g => g.Name.ToLower().Contains(filter.NameIncludeSubstring.ToLower()));
      }

      return (
        await query.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await query.CountAsync());
    }
  }
}
