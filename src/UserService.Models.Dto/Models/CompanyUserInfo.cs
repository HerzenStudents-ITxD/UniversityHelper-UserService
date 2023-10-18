using HerzenHelper.Models.Broker.Enums;
using HerzenHelper.Models.Broker.Models.Company;
using System;

namespace HerzenHelper.UserService.Models.Dto.Models
{
  public record CompanyUserInfo
  {
    public CompanyInfo Company { get; set; }
    //public ContractSubjectData ContractSubject { get; set; }
    //public ContractTerm ContractTermType { get; set; }
    public double? Rate { get; set; }
    public DateTime StartWorkingAt { get; set; }
    public DateTime? EndWorkingAt { get; set; }
    public DateTime? Probation { get; set; }
  }
}
