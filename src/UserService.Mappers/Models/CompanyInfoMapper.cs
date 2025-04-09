using UniversityHelper.Models.Broker.Models.University;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models;

public class UniversityInfoMapper : IUniversityInfoMapper
{
  public UniversityInfo Map(UniversityData universityData)
  {
    if (universityData is null)
    {
      return null;
    }

    return new UniversityInfo
    {
      Id = universityData.Id,
      //Name = universityData.Name
    };
  }
}
