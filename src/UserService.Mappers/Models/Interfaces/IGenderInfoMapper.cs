using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using System.Collections.Generic;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces;

[AutoInject]
public interface IGenderInfoMapper
{
  List<GenderInfo> Map(List<DbGender> dbGenders);
}
