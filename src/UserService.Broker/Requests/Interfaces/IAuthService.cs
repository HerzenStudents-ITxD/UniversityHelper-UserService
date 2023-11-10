using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IAuthService
  {
    Task<IGetTokenResponse> GetTokenAsync(Guid userId, List<string> errors);
  }
}
