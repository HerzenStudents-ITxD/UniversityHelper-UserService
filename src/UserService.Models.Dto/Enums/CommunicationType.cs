using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UniversityHelper.UserService.Models.Dto.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum CommunicationType
{
  Email,
  Telegram,
  Phone,
  VK,
  BaseEmail
}
