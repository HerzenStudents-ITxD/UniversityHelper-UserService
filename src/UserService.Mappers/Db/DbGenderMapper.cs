using UniversityHelper.Core.Extensions;
using UniversityHelper.UserService.Mappers.Db.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Gender;
using Microsoft.AspNetCore.Http;
using System;

namespace UniversityHelper.UserService.Mappers.Db
{
  public class DbGenderMapper : IDbGenderMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbGenderMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbGender Map(CreateGenderRequest request)
    {
      return request is null
        ? null
        : new DbGender
        {
          Id = Guid.NewGuid(),
          Name = request.Name,
          CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
          CreatedAtUtc = DateTime.UtcNow,
        };
    }
  }
}
