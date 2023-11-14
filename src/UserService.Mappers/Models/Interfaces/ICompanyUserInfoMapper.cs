using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Company;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces;

[AutoInject]
public interface ICompanyUserInfoMapper
{
  CompanyUserInfo Map(CompanyData companyData, CompanyUserData companyUserData);
}
