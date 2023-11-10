using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.User;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.UserService.Mappers.Patch.Interfaces
{
  [AutoInject]
  public interface IPatchDbUserMapper
  {
    (JsonPatchDocument<DbUser> dbUserPatch, JsonPatchDocument<DbUserAddition> dbUserAdditionPatch) Map(
     JsonPatchDocument<EditUserRequest> request);
  }
}