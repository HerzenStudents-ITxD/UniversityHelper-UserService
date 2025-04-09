using UniversityHelper.Core.Attributes;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IEmailService
{
  Task SendAsync(string email, string subject, string text, List<string> errors);
}
