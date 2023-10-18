using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.Right;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IRightService
  {
    Task<List<RoleData>> GetRolesAsync(
      Guid userId,
      string locale,
      List<string> errors,
      CancellationToken cancellationToken = default);
  }
}
