using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.User;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Interfaces;

/// <summary>
/// Represents interface for a command in command pattern.
/// Provides method for editing an existing user.
/// </summary>
[AutoInject]
public interface IEditUserCommand
{
  /// <summary>
  /// Editing an existing user. Returns true if it succeeded to edit a user, otherwise false.
  /// </summary>
  Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, JsonPatchDocument<EditUserRequest> patch);
}
