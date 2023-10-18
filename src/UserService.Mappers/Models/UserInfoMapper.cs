using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Enums;
using HerzenHelper.UserService.Models.Dto.Models;
using System.Linq;

namespace HerzenHelper.UserService.Mappers.Models
{
  public class UserInfoMapper : IUserInfoMapper
  {
    public UserInfo Map(
      DbUser dbUser,
      ImageInfo avatar)
    {
      return dbUser is null ? default : new UserInfo
      {
        Id = dbUser.Id,
        FirstName = dbUser.FirstName,
        LastName = dbUser.LastName,
        MiddleName = dbUser.MiddleName,
        IsAdmin = dbUser.IsAdmin,
        IsActive = dbUser.IsActive,
        PendingInfo = dbUser.Pending is null ? null : new PendingUserInfo()
        { InvitationCommunicationId = dbUser.Pending.CommunicationId },
        Avatar = avatar,
        Communications = dbUser.Communications
          ?.Select(c => new CommunicationInfo
          {
            Id = c.Id,
            Type = (CommunicationType)c.Type,
            Value = c.Value,
            IsConfirmed = c.IsConfirmed
          }),
      };
    }
  }
}
