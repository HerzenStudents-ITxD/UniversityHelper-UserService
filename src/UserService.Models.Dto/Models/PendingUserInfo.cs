using System;

namespace HerzenHelper.UserService.Models.Dto.Models
{
  public record PendingUserInfo
  {
    public Guid InvitationCommunicationId { get; set; }
  }
}
