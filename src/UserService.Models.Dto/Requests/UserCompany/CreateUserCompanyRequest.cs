namespace UniversityHelper.UserService.Models.Dto.Requests.UserUniversity;

public record CreateUserUniversityRequest
{
  public Guid UniversityId { get; set; }
  public Guid? ContractSubjectId { get; set; }
  //public ContractTerm ContractTermType { get; set; }
  public double? Rate { get; set; }
  public DateTime StartWorkingAt { get; set; }
  public DateTime? EndWorkingAt { get; set; }
  public DateTime? Probation { get; set; }
}
