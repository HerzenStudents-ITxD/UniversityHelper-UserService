using System;

namespace UniversityHelper.UserService.Models.Dto.Models
{
  public record PendingUserInfo
  {
    public Guid InvitationCommunicationId { get; set; }
  }
}
