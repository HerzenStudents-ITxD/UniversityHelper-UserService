﻿namespace UniversityHelper.UserService.Models.Dto.Models;

  public record RoleInfo
  {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public List<int> RightsIds { get; set; }
  }
