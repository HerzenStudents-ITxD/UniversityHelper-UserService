﻿using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.UserService.Business.Commands.Communication.Interfaces;

  [AutoInject]
  public interface IRemoveCommunicationCommand
  {
      Task<OperationResultResponse<bool>> ExecuteAsync(Guid communicationId);
  }
