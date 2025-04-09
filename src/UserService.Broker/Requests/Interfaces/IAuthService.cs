using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Responses.Auth;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IAuthService
{
  Task<IGetTokenResponse> GetTokenAsync(Guid userId, List<string> errors);
}
