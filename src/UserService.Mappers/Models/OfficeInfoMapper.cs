﻿using UniversityHelper.Models.Broker.Models.Office;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models;

public class OfficeInfoMapper : IOfficeInfoMapper
{
  public OfficeInfo Map(OfficeData office)
  {
    return office is null
      ? default
      : new OfficeInfo
      {
        Id = office.Id,
        Name = office.Name,
        Address = office.Address,
        City = office.City
      };
  }
}
