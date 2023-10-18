using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using System;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Communication.Interfaces
{
    [AutoInject]
    public interface IRemoveCommunicationCommand
    {
        Task<OperationResultResponse<bool>> ExecuteAsync(Guid communicationId);
    }
}
