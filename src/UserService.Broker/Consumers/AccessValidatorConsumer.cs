using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Requests;
using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.UserService.Data.Interfaces;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Consumers;

public class AccessValidatorConsumer : IConsumer<ICheckUserIsAdminRequest>
{
  private readonly IUserRepository _repository;

  public AccessValidatorConsumer(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task Consume(ConsumeContext<ICheckUserIsAdminRequest> context)
  {
    var response = OperationResultWrapper.CreateResponse(IsAdminAsync, context.Message.UserId);

    await context.RespondAsync<IOperationResult<bool>>(response);
  }

  public async Task<object> IsAdminAsync(Guid userId)
  {
    return (await _repository.GetAsync(userId)).IsAdmin;
  }
}
