using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using System;

namespace UniversityHelper.UserService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserCommunicationMapper
  {
    DbUserCommunication Map(CreateCommunicationRequest request, Guid? userId = null);
  }
}
