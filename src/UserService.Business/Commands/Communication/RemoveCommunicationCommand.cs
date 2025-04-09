using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Business.Commands.Communication.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Enums;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace UniversityHelper.UserService.Business.Commands.Communication;

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
