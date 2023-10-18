using HerzenHelper.Core.BrokerSupport.Attributes;
using HerzenHelper.Core.BrokerSupport.Configurations;
using HerzenHelper.Models.Broker.Requests.Auth;
using HerzenHelper.Models.Broker.Requests.Company;
using HerzenHelper.Models.Broker.Requests.Department;
using HerzenHelper.Models.Broker.Requests.Email;
using HerzenHelper.Models.Broker.Requests.Image;
using HerzenHelper.Models.Broker.Requests.Office;
using HerzenHelper.Models.Broker.Requests.Position;
using HerzenHelper.Models.Broker.Requests.Rights;
using HerzenHelper.Models.Broker.Requests.TextTemplate;
using HerzenHelper.Models.Broker.Requests.User;

namespace HerzenHelper.UserService.Models.Dto.Configurations
{
  public class RabbitMqConfig : ExtendedBaseRabbitMqConfig
  {
    public string GetUserCredentialsEndpoint { get; set; }
    public string GetUsersDataEndpoint { get; set; }
    public string CreateAdminEndpoint { get; set; }
    public string FindParseEntitiesEndpoint { get; set; }
    public string CheckUsersExistenceEndpoint { get; set; }
    public string FilterUsersDataEndpoint { get; set; }

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
}
