using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Models;
using System.Collections.Generic;
using System.Linq;

namespace HerzenHelper.UserService.Mappers.Models
{
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
}
