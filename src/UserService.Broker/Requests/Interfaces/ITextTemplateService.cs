using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Enums;
using UniversityHelper.Models.Broker.Responses.TextTemplate;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface ITextTemplateService
{
  Task<IGetTextTemplateResponse> GetAsync(
    TemplateType templateType,
    string locale,
    List<string> errors,
    Guid? endpointId = null);
}
