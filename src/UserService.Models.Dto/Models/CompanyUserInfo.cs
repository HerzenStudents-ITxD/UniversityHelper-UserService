namespace UniversityHelper.UserService.Models.Dto.Models;

public record UniversityUserInfo
{
  public UniversityInfo University { get; set; }
  //public ContractSubjectData ContractSubject { get; set; }
  //public ContractTerm ContractTermType { get; set; }
  public double? Rate { get; set; }
  public DateTime StartWorkingAt { get; set; }
  public DateTime? EndWorkingAt { get; set; }
  public DateTime? Probation { get; set; }
}
