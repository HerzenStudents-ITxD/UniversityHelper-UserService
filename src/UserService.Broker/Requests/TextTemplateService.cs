using HerzenHelper.Core.BrokerSupport.Helpers;
using HerzenHelper.Models.Broker.Enums;
using HerzenHelper.Models.Broker.Requests.TextTemplate;
using HerzenHelper.Models.Broker.Responses.TextTemplate;
using HerzenHelper.UserService.Broker.Requests.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Requests
{
  public class TextTemplateService : ITextTemplateService
  {
    private readonly IRequestClient<IGetTextTemplateRequest> _rcGetTextTemplate;
    private readonly ILogger<TextTemplateService> _logger;

    public TextTemplateService(
      IRequestClient<IGetTextTemplateRequest> rcGetTextTemplate,
      ILogger<TextTemplateService> logger)
    {
      _rcGetTextTemplate = rcGetTextTemplate;
      _logger = logger;
    }

    public async Task<IGetTextTemplateResponse> GetAsync(
      TemplateType templateType,
      string locale,
      List<string> errors,
      Guid? endpointId = null)
    {
      return await RequestHandler.ProcessRequest<IGetTextTemplateRequest, IGetTextTemplateResponse>(
        _rcGetTextTemplate,
        IGetTextTemplateRequest.CreateObj(
          endpointId: (Guid)endpointId,
          templateType: templateType,
          locale: locale),
        errors,
        _logger);
    }
  }
}
