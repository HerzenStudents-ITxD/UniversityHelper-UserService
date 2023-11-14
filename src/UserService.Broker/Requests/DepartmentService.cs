using UniversityHelper.Core.BrokerSupport.Helpers;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Extensions;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Models.Broker.Models.Department;
using UniversityHelper.Models.Broker.Requests.Department;
using UniversityHelper.Models.Broker.Responses.Department;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests;

public class DepartmentService : IDepartmentService
{
  private readonly IRequestClient<IGetDepartmentsRequest> _rcGetDepartments;
  private readonly ILogger<DepartmentService> _logger;
  private readonly IGlobalCacheRepository _globalCache;

  public DepartmentService(
    IRequestClient<IGetDepartmentsRequest> rcGetDepartments,
    ILogger<DepartmentService> logger,
    IGlobalCacheRepository globalCache)
  {
    _rcGetDepartments = rcGetDepartments;
    _logger = logger;
    _globalCache = globalCache;
  }

  public async Task<List<DepartmentData>> GetDepartmentsAsync(
    Guid userId,
    List<string> errors,
    bool includeChildDepartmentsIds = false,
    CancellationToken cancellationToken = default)
  {
    object request = IGetDepartmentsRequest.CreateObj(usersIds: new() { userId }//, 
      //includeChildDepartmentsIds: includeChildDepartmentsIds
      );

    List<DepartmentData> departments = await _globalCache
      .GetAsync<List<DepartmentData>>(Cache.Communities, userId.GetRedisCacheKey(nameof(IGetDepartmentsRequest), request.GetBasicProperties()));

    if (departments is not null)
    {
      _logger.LogInformation(
        "Departments for user id {UserId} were taken from cache.",
        userId);
    }
    else
    {
      departments = (await RequestHandler.ProcessRequest<IGetDepartmentsRequest, IGetDepartmentsResponse>(
          _rcGetDepartments,
          request,
          errors,
          _logger))
        ?.Departments;
    }

    return departments;
  }
}
