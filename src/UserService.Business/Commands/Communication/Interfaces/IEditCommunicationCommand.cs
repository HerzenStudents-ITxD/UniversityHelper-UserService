using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Requests.Communication;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Communication.Interfaces
{
  [AutoInject]
  public interface IEditCommunicationCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid communicationId, EditCommunicationRequest request);
  }
}
