using HerzenHelper.Core.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IEmailService
  {
    Task SendAsync(string email, string subject, string text, List<string> errors);
  }
}
