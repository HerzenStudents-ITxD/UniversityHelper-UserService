using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto;

namespace UniversityHelper.UserService.Business.Commands.Password.Interfaces;

[AutoInject]
public interface IReconstructPasswordCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(ReconstructPasswordRequest request);
}
