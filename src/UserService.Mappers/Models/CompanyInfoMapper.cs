﻿using UniversityHelper.Models.Broker.Models.Company;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models;

public class CompanyInfoMapper : ICompanyInfoMapper
{
  public CompanyInfo Map(CompanyData companyData)
  {
    if (companyData is null)
    {
      return null;
    }

    return new CompanyInfo
    {
      Id = companyData.Id,
      Name = companyData.Name
    };
  }
}
