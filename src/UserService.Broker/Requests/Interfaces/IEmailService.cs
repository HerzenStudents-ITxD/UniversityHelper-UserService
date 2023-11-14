using UniversityHelper.Core.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IEmailService
{
  Task SendAsync(string email, string subject, string text, List<string> errors);
}
