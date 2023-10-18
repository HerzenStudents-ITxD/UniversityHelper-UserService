using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto.Requests.Communication;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Communication.Interfaces
{
  [AutoInject]
  public interface ICreateCommunicationCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateCommunicationRequest request);
  }
}
