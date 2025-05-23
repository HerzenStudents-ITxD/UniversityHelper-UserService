﻿using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.Models.Broker.Common;
using UniversityHelper.UserService.Data.Interfaces;
using MassTransit;

namespace UniversityHelper.UserService.Broker.Consumers;

public class CheckUsersExistenceConsumer : IConsumer<ICheckUsersExistence>
{
  private readonly IUserRepository _repository;

  public CheckUsersExistenceConsumer(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task Consume(ConsumeContext<ICheckUsersExistence> context)
  {
    var response = OperationResultWrapper.CreateResponse(GetUsersExistenceInfoAsync, context.Message);

    await context.RespondAsync<IOperationResult<ICheckUsersExistence>>(response);
  }

  public async Task<object> GetUsersExistenceInfoAsync(ICheckUsersExistence requestIds)
  {
    List<Guid> userIds = await _repository.AreExistingIdsAsync(requestIds.UserIds);

    return ICheckUsersExistence.CreateObj(userIds);
  }
}
