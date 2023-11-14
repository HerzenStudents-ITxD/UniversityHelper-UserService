using UniversityHelper.Core.BrokerSupport.Helpers;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Extensions;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Models.Broker.Models.Company;
using UniversityHelper.Models.Broker.Requests.Company;
using UniversityHelper.Models.Broker.Responses.Company;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests;

public class CompanyService : ICompanyService
{
  private readonly IRequestClient<IGetCompaniesRequest> _rcGetCompanies;
  private readonly ILogger<CompanyService> _logger;
  private readonly IGlobalCacheRepository _globalCache;

  public CompanyService(
    IRequestClient<IGetCompaniesRequest> rcGetCompanies,
    ILogger<CompanyService> logger,
    IGlobalCacheRepository globalCache)
  {
    _rcGetCompanies = rcGetCompanies;
    _logger = logger;
    _globalCache = globalCache;
  }

  public async Task<List<CompanyData>> GetCompaniesAsync(
    Guid userId,
    List<string> errors,
    CancellationToken cancellationToken = default)
  {
    object request = IGetCompaniesRequest.CreateObj(usersIds: new() { userId });

    List<CompanyData> companies = await _globalCache
      .GetAsync<List<CompanyData>>(Cache.Communities, userId.GetRedisCacheKey(nameof(IGetCompaniesRequest), request.GetBasicProperties()));

    if (companies is not null)
    {
      _logger.LogInformation(
        "Companies for user id '{UserId}' were taken from cache.",
        userId);
    }
    else
    {
      companies = (await RequestHandler.ProcessRequest<IGetCompaniesRequest, IGetCompaniesResponse>(
          _rcGetCompanies,
          request,
          errors,
          _logger))
        ?.Companies;
    }

    return companies;
  }
}
