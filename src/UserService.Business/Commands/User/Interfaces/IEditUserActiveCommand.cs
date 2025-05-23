﻿using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.User;

namespace UniversityHelper.UserService.Business.Commands.User.Interfaces;

[AutoInject]
public interface IEditUserActiveCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(EditUserActiveRequest request);
}
