﻿using UniversityHelper.Core.BrokerSupport.Attributes;
using UniversityHelper.Core.BrokerSupport.Configurations;
using UniversityHelper.Models.Broker.Requests.Auth;
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
  public string CheckUserIsAdminEndpoint { get; set; }
  public string GetUsersDataEndpoint { get; set; }
  public string GetUserCredentialsEndpoint { get; set; }
  public string SearchUsersEndpoint { get; set; }
  public string CreateAdminEndpoint { get; set; }
  public string FindParseEntitiesEndpoint { get; set; }
  public string CheckUsersExistenceEndpoint { get; set; }

  // Request Clients
  [AutoInjectRequest(typeof(IGetTextTemplateRequest))]
  public string GetTextTemplateEndpoint { get; set; }

  [AutoInjectRequest(typeof(ISendEmailRequest))]
  public string SendEmailEndpoint { get; set; }

  [AutoInjectRequest(typeof(IGetTokenRequest))]
  public string GetTokenEndpoint { get; set; }

  [AutoInjectRequest(typeof(IGetUserRolesRequest))]
  public string GetUserRolesEndpoint { get; set; }

  [AutoInjectRequest(typeof(IGetPositionsRequest))]
  public string GetPositionsEndpoint { get; set; }

  [AutoInjectRequest(typeof(IGetOfficesRequest))]
  public string GetOfficesEndpoint { get; set; }

  [AutoInjectRequest(typeof(ICreateImagesRequest))]
  public string CreateImagesEndpoint { get; set; }

  [AutoInjectRequest(typeof(IGetImagesRequest))]
  public string GetImagesEndpoint { get; set; }
}
