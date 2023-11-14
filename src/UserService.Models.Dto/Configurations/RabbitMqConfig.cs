using UniversityHelper.Core.BrokerSupport.Attributes;
using UniversityHelper.Core.BrokerSupport.Configurations;
using UniversityHelper.Models.Broker.Requests.Auth;
using UniversityHelper.Models.Broker.Requests.Company;
using UniversityHelper.Models.Broker.Requests.Department;
using UniversityHelper.Models.Broker.Requests.Email;
using UniversityHelper.Models.Broker.Requests.Image;
using UniversityHelper.Models.Broker.Requests.Office;
using UniversityHelper.Models.Broker.Requests.Position;
using UniversityHelper.Models.Broker.Requests.Rights;
using UniversityHelper.Models.Broker.Requests.TextTemplate;
using UniversityHelper.Models.Broker.Requests.User;

namespace UniversityHelper.UserService.Models.Dto.Configurations;

public class RabbitMqConfig : ExtendedBaseRabbitMqConfig
{
  //public string GetUserCredentialsEndpoint { get; set; }
  //public string GetUsersDataEndpoint { get; set; }
  //public string CreateAdminEndpoint { get; set; }
  //public string FindParseEntitiesEndpoint { get; set; }
  //public string CheckUsersExistenceEndpoint { get; set; }
  //public string FilterUsersDataEndpoint { get; set; }

  //TextTemplate

  [AutoInjectRequest(typeof(IGetTextTemplateRequest))]
  public string GetTextTemplateEndpoint { get; set; }

  //Email

  [AutoInjectRequest(typeof(ISendEmailRequest))]
  public string SendEmailEndpoint { get; set; }

  //Auth

  [AutoInjectRequest(typeof(IGetTokenRequest))]
  public string GetTokenEndpoint { get; set; }

  //Search

  [AutoInjectRequest(typeof(ISearchUsersRequest))]
  public string SearchUsersEndpoint { get; set; }

  //Rights

  [AutoInjectRequest(typeof(IGetUserRolesRequest))]
  public string GetUserRolesEndpoint { get; set; }

  //Position

  [AutoInjectRequest(typeof(IGetPositionsRequest))]
  public string GetPositionsEndpoint { get; set; }

  //Department

  [AutoInjectRequest(typeof(IGetDepartmentsRequest))]
  public string GetDepartmentsEndpoint { get; set; }

  //Company

  [AutoInjectRequest(typeof(IGetCompaniesRequest))]
  public string GetCompaniesEndpoint { get; set; }

  //Office

  [AutoInjectRequest(typeof(IGetOfficesRequest))]
  public string GetOfficesEndpoint { get; set; }

  //Image

  [AutoInjectRequest(typeof(ICreateImagesRequest))]
  public string CreateImagesEndpoint { get; set; }

  [AutoInjectRequest(typeof(IGetImagesRequest))]
  public string GetImagesEndpoint { get; set; }
}
