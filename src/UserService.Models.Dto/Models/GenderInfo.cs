﻿using System;

namespace UniversityHelper.UserService.Models.Dto.Models;

public record GenderInfo
{
  public Guid Id { get; set; }
  public string Name { get; set; }
}
