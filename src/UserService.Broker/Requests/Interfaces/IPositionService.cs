using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models.Position;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IPositionService
  {
    Task<List<PositionData>> GetPositionsAsync(Guid userId, List<string> errors, CancellationToken cancellationToken = default);
  }
}
