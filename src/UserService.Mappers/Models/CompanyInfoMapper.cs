using HerzenHelper.Models.Broker.Models.Company;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models
{
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
}
