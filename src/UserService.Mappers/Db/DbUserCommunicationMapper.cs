using UniversityHelper.Core.Extensions;
using UniversityHelper.UserService.Mappers.Db.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using Microsoft.AspNetCore.Http;
using System;

namespace UniversityHelper.UserService.Mappers.Db;

public class DbUserCommunicationMapper : IDbUserCommunicationMapper
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public DbUserCommunicationMapper(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public DbUserCommunication Map(CreateCommunicationRequest request, Guid? userId = null)
  {
    return request is null
      ? null
      : new DbUserCommunication
      {
        Id = Guid.NewGuid(),
        UserId = request.UserId.HasValue ? request.UserId.Value : userId.Value,
        Type = (int)request.Type,
        Value = request.Value,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
      };
  }
}
