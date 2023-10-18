using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HerzenHelper.UserService.Models.Dto.Enums
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum CommunicationType
  {
    Email,
    Telegram,
    Phone,
    Skype,
    Twitter,
    BaseEmail
  }
}
