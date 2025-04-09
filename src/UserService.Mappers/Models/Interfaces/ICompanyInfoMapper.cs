using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.University;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces;

[AutoInject]
public interface IUniversityInfoMapper
{
  UniversityInfo Map(UniversityData universityData);
}
