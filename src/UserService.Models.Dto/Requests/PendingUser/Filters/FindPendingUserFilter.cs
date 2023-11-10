using UniversityHelper.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.UserService.Models.Dto.Requests.PendingUser.Filters
{
  public record FindPendingUserFilter : BaseFindFilter
  {
    [FromQuery(Name = "includecommunication")]
    public bool IncludeCommunication { get; set; } = false;

    [FromQuery(Name = "includecurrentavatar")]
    public bool IncludeCurrentAvatar { get; set; } = false;
  }
}
