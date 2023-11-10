using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Enums;
using UniversityHelper.Models.Broker.Responses.TextTemplate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface ITextTemplateService
  {
    Task<IGetTextTemplateResponse> GetAsync(
      TemplateType templateType,
      string locale,
      List<string> errors,
      Guid? endpointId = null);
  }
}
