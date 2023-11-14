using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Office;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces;

[AutoInject]
  public interface IOfficeInfoMapper
  {
      OfficeInfo Map(OfficeData office);
  }
