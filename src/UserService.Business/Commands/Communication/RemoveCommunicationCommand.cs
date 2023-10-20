using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Constants;
using HerzenHelper.Core.Enums;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.Communication.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Communication
{
  public class RemoveCommunicationCommand : IRemoveCommunicationCommand
  {
    //private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserCommunicationRepository _repository;
    private readonly IResponseCreator _responseCreator;

    public RemoveCommunicationCommand(
      //IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IUserCommunicationRepository communicationRepository,
      IResponseCreator responseCreator)
    {
      //_accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _repository = communicationRepository;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid communicationId)
    {
      DbUserCommunication dbUserCommunication = await _repository.GetAsync(communicationId);

      //if ((_httpContextAccessor.HttpContext.GetUserId() != dbUserCommunication.UserId) &&
      //  !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      //{
      //  return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      //}

      if (dbUserCommunication.Type == (int)CommunicationType.BaseEmail)
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          new List<string>() { "Base email cannot be removed." });
      }

      return new OperationResultResponse<bool>(
        body: await _repository.RemoveAsync(dbUserCommunication));
    }
  }
}
