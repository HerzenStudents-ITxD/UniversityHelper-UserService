using UniversityHelper.Models.Broker.Enums;
using System;

namespace UniversityHelper.UserService.Models.Dto.Requests.UserCompany
{
  public record CreateUserCompanyRequest
  {
    public Guid CompanyId { get; set; }
    public Guid? ContractSubjectId { get; set; }
    //public ContractTerm ContractTermType { get; set; }
    public double? Rate { get; set; }
    public DateTime StartWorkingAt { get; set; }
    public DateTime? EndWorkingAt { get; set; }
    public DateTime? Probation { get; set; }
  }
}
