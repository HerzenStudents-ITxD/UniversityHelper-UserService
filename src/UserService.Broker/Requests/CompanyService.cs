using UniversityHelper.Core.BrokerSupport.Helpers;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Extensions;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using UniversityHelper.Models.Broker.Requests.University;
using UniversityHelper.Models.Broker.Models.University;
using UniversityHelper.Models.Broker.Responses.University;

namespace UniversityHelper.UserService.Broker.Requests;

public class UniversityService : IUniversityService
{
  private readonly IRequestClient<IGetUniversitiesRequest> _rcGetUniversities;
  private readonly ILogger<UniversityService> _logger;
  private readonly IGlobalCacheRepository _globalCache;

  public UniversityService(
    IRequestClient<IGetUniversitiesRequest> rcGetUniversities,
    ILogger<UniversityService> logger,
    IGlobalCacheRepository globalCache)
  {
    _rcGetUniversities = rcGetUniversities;
    _logger = logger;
    _globalCache = globalCache;
  }

  public async Task<List<UniversityData>> GetUniversitiesAsync(
    Guid userId,
    List<string> errors,
    CancellationToken cancellationToken = default)
  {
    object request = IGetUniversitiesRequest.CreateObj(usersIds: new() { userId });

    List<UniversityData> companies = await _globalCache
      .GetAsync<List<UniversityData>>(Cache.Communities, userId.GetRedisCacheKey(nameof(IGetUniversitiesRequest), request.GetBasicProperties()));

    if (companies is not null)
    {
      _logger.LogInformation(
        "Universities for user id '{UserId}' were taken from cache.",
        userId);
    }
    else
    {
      companies = (await RequestHandler.ProcessRequest<IGetUniversitiesRequest, IGetUniversitiesResponse>(
          _rcGetUniversities,
          request,
          errors,
          _logger))
        ?.Universities;
    }

    return companies;
  }
}
