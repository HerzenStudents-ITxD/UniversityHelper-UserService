using HerzenHelper.Models.Broker.Enums;

namespace HerzenHelper.UserService.Models.Dto.Models
{
  public record DepartmentUserInfo
  {
    public DepartmentInfo Department { get; set; }
    //public DepartmentUserRole Role { get; set; }
  }
}
