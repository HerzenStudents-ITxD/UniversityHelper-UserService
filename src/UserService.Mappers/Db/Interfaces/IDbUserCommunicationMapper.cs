using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Communication;
using System;

namespace HerzenHelper.UserService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserCommunicationMapper
  {
    DbUserCommunication Map(CreateCommunicationRequest request, Guid? userId = null);
  }
}
