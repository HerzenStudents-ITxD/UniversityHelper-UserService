using UniversityHelper.Core.BrokerSupport.Helpers;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Extensions;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Models.Broker.Models.Position;
using UniversityHelper.Models.Broker.Requests.Position;
using UniversityHelper.Models.Broker.Responses.Position;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests
{
  public class PositionService : IPositionService
  {
    private readonly IRequestClient<IGetPositionsRequest> _rcGetPositions;
    private readonly ILogger<PositionService> _logger;
    private readonly IGlobalCacheRepository _globalCache;

    public PositionService(
      IRequestClient<IGetPositionsRequest> rcGetPositions,
      ILogger<PositionService> logger,
      IGlobalCacheRepository globalCache)
    {
      _rcGetPositions = rcGetPositions;
      _logger = logger;
      _globalCache = globalCache;
    }

    public async Task<List<PositionData>> GetPositionsAsync(
      Guid userId,
      List<string> errors,
      CancellationToken cancellationToken = default)
    {
      object request = IGetPositionsRequest.CreateObj(usersIds: new() { userId });

      List<PositionData> positions = await _globalCache
        .GetAsync<List<PositionData>>(Cache.Events, userId.GetRedisCacheKey(nameof(IGetPositionsRequest), request.GetBasicProperties()));

      if (positions is not null)
      {
        _logger.LogInformation(
          "Positions for user id '{UserId}' were taken from cache.",
          userId);
      }
      else
      {
        positions = (await RequestHandler.ProcessRequest<IGetPositionsRequest, IGetPositionsResponse>(
            _rcGetPositions,
            request,
            errors,
            _logger))
          ?.Positions;
      }

      return positions;
    }
  }
}
