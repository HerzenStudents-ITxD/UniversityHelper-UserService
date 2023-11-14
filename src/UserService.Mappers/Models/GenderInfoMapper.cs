using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityHelper.UserService.Mappers.Models;

public class GenderInfoMapper : IGenderInfoMapper
{
  public List<GenderInfo> Map(List<DbGender> dbGenders)
  {
    return dbGenders is null
      ? default
      : dbGenders.Select(x => new GenderInfo 
        {
          Id = x.Id,
          Name = x.Name
        }).ToList();
  }
}
