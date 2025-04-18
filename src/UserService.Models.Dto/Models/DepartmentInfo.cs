﻿namespace UniversityHelper.UserService.Models.Dto.Models;

public record DepartmentInfo
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string ShortName { get; set; }
  public List<Guid> ChildDepartmentsIds { get; set; }
}
