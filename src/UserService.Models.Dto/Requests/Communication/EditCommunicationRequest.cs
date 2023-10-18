using HerzenHelper.UserService.Models.Dto.Enums;

namespace HerzenHelper.UserService.Models.Dto.Requests.Communication
{
  public record EditCommunicationRequest
  {
    public CommunicationType? Type { get; set; }
    public string Value { get; set; }
  }
}
