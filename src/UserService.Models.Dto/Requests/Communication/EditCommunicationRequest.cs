using UniversityHelper.UserService.Models.Dto.Enums;

namespace UniversityHelper.UserService.Models.Dto.Requests.Communication
{
  public record EditCommunicationRequest
  {
    public CommunicationType? Type { get; set; }
    public string Value { get; set; }
  }
}
