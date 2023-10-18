using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Enums;
using HerzenHelper.Models.Broker.Responses.TextTemplate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Requests.Interfaces
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
