using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;
using UniversityHelper.UserService.Models.Dto.Responses.Credentials;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Credentials.Interfaces;

[AutoInject]
public interface IReactivateCredentialsCommand
{
  Task<OperationResultResponse<CredentialsResponse>> ExecuteAsync(ReactivateCredentialsRequest request);
}
