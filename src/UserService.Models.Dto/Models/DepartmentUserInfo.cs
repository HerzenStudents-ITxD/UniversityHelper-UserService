using UniversityHelper.Models.Broker.Enums;

namespace UniversityHelper.UserService.Models.Dto.Models;

public record DepartmentUserInfo
{
  public DepartmentInfo Department { get; set; }
  //public DepartmentUserRole Role { get; set; }
}
