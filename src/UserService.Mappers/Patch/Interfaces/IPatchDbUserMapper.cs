using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.User;
using Microsoft.AspNetCore.JsonPatch;

namespace HerzenHelper.UserService.Mappers.Patch.Interfaces
{
  [AutoInject]
  public interface IPatchDbUserMapper
  {
    (JsonPatchDocument<DbUser> dbUserPatch, JsonPatchDocument<DbUserAddition> dbUserAdditionPatch) Map(
     JsonPatchDocument<EditUserRequest> request);
  }
}