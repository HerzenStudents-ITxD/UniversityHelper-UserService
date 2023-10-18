using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Models.Broker.Common;
using HerzenHelper.UserService.Data.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Consumers
{
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
}
