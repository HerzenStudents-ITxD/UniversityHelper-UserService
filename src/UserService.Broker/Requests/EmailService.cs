﻿using UniversityHelper.Core.BrokerSupport.Helpers;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Models.Broker.Requests.Email;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UniversityHelper.UserService.Broker.Requests;

public class EmailService : IEmailService
{
  private readonly IRequestClient<ISendEmailRequest> _rcSendEmail;
  private readonly ILogger<EmailService> _logger;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public EmailService(
    IRequestClient<ISendEmailRequest> rcSendEmail,
    ILogger<EmailService> logger,
    IHttpContextAccessor httpContextAccessor)
  {
    _rcSendEmail = rcSendEmail;
    _logger = logger;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task SendAsync(string email, string subject, string text, List<string> errors)
  {
    if (!await RequestHandler.ProcessRequest<ISendEmailRequest, bool>(
      _rcSendEmail,
      ISendEmailRequest.CreateObj(
        email,
        subject,
        text,
        _httpContextAccessor.HttpContext.UserIdOrDefault()),
      errors,
      _logger))
    {
      _logger.LogError(
        "Letter not sent to email '{Email}'",
        email);

      errors.Add($"Can not send email to '{email}'. Email placed in resend queue and will be resent in 1 hour.");
    }
  }
}
