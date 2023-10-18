using HerzenHelper.Core.Attributes;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Models.Dto;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Password.Interfaces
{
  [AutoInject]
  public interface IReconstructPasswordCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(ReconstructPasswordRequest request);
  }
}
