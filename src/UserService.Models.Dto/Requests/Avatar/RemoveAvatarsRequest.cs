﻿using System;
using System.Collections.Generic;

namespace UniversityHelper.UserService.Models.Dto.Requests.Avatar;

public record RemoveAvatarsRequest
{
  public Guid UserId { get; set; }
  public List<Guid> AvatarsIds { get; set; }
}
