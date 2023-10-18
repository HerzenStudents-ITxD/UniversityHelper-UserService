using HerzenHelper.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace HerzenHelper.UserService.Models.Dto.Requests.Filtres
{
  public record FindUsersFilter : BaseFindFilter
  {
    [FromQuery(Name = "isascendingsort")]
    public bool? IsAscendingSort { get; set; }

    [FromQuery(Name = "fullnameincludesubstring")]
    public string FullNameIncludeSubstring { get; set; }

    [FromQuery(Name = "isactive")]
    public bool? IsActive { get; set; }

    [FromQuery(Name = "ispending")]
    public bool? IsPending { get; set; }

    [FromQuery(Name = "includecurrentavatar")]
    public bool IncludeCurrentAvatar { get; set; } = false;

    [FromQuery(Name = "includecommunications")]
    public bool IncludeCommunications { get; set; } = false;
  }
}
