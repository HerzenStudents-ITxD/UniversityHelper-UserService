using UniversityHelper.UserService.Mappers.Patch.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Models.Dto.Requests.User;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;

namespace UniversityHelper.UserService.Mappers.Patch;

public class PatchDbUserMapper : IPatchDbUserMapper
{
  public (JsonPatchDocument<DbUser> dbUserPatch, JsonPatchDocument<DbUserAddition> dbUserAdditionPatch) Map(
   JsonPatchDocument<EditUserRequest> request)
  {
    if (request is null)
    {
      return (null, null);
    }

    JsonPatchDocument<DbUser> dbUserPatch = new();
    JsonPatchDocument<DbUserAddition> dbUserAdditionPatch = new();

    Func<Operation<EditUserRequest>, string> value = item => !string.IsNullOrEmpty(item.value?.ToString().Trim())
        ? item.value.ToString().Trim() : null;

    foreach (Operation<EditUserRequest> item in request.Operations)
    {
      if (item.path.EndsWith(nameof(EditUserRequest.FirstName)) ||
       item.path.EndsWith(nameof(EditUserRequest.LastName)) ||
       item.path.EndsWith(nameof(EditUserRequest.MiddleName)) ||
       item.path.EndsWith(nameof(EditUserRequest.IsAdmin)))
      {
        dbUserPatch.Operations.Add(new Operation<DbUser>(item.op, item.path, item.from, item.value));
        continue;
      }

      if (item.path.EndsWith(nameof(EditUserRequest.Status), StringComparison.OrdinalIgnoreCase))
      {
        dbUserPatch.Operations.Add(new Operation<DbUser>(item.op, item.path, item.from, (int)Enum.Parse(typeof(UserStatus), item.value.ToString())));
        continue;
      }      

      dbUserAdditionPatch.Operations.Add(new Operation<DbUserAddition>(item.op, item.path, item.from, String.IsNullOrEmpty(item.value?.ToString())? null : item.value));
    }

    return (dbUserPatch, dbUserAdditionPatch);
  }
}