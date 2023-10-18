using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models.Company;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface ICompanyInfoMapper
  {
    CompanyInfo Map(CompanyData companyData);
  }
}
