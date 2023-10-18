using HerzenHelper.Core.Extensions;
using HerzenHelper.UserService.Mappers.Db.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Gender;
using Microsoft.AspNetCore.Http;
using System;

namespace HerzenHelper.UserService.Mappers.Db
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
