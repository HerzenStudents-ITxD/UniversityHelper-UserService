using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IAuthService
  {
    Task<IGetTokenResponse> GetTokenAsync(Guid userId, List<string> errors);
  }
}
