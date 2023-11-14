using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UniversityHelper.UserService.Models.Dto.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum UserStatus
{
  Vacation,
  Sick,
  WorkFromOffice,
  WorkFromHome
}
