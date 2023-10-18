using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IUserInfoMapper
  {
    UserInfo Map(
      DbUser dbUser,
      ImageInfo avatar);
  }
}
