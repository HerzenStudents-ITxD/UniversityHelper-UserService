using System;
using System.Collections.Generic;

namespace HerzenHelper.UserService.Models.Dto.Requests.User.Avatar
{
  public record RemoveAvatarsRequest
  {
    public Guid UserId { get; set; }
    public List<Guid> AvatarsIds { get; set; }
  }
}
