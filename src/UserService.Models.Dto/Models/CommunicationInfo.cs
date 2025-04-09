using UniversityHelper.UserService.Models.Dto.Enums;

namespace UniversityHelper.UserService.Models.Dto.Models;

public record CommunicationInfo
{
  public Guid Id { get; set; }
  public CommunicationType Type { get; set; }
  public string Value { get; set; }
  public bool IsConfirmed { get; set; }
}
