using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Models;
using System.Collections.Generic;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IGenderInfoMapper
  {
    List<GenderInfo> Map(List<DbGender> dbGenders);
  }
}
