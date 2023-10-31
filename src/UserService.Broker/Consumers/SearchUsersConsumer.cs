using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.User;
using HerzenHelper.Models.Broker.Requests.User;
using HerzenHelper.Models.Broker.Responses.Search;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Models.Db;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Consumers
{
  public class SearchUsersConsumer : IConsumer<ISearchUsersRequest>
  {
    private IUserRepository _userRepository;

    private async Task<object> SearchUsersAsync(string searchText)
    {
      List<DbUser> users = await _userRepository.SearchAsync(searchText).ToListAsync();

      return new();
      //return ISearchResponse.CreateObj(
      //  users.Select(
      //    u => new UserSearchData(u.Id, u.FirstName, u.LastName, u.MiddleName)).ToList());
    }

    public SearchUsersConsumer(
      IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<ISearchUsersRequest> context)
    {
      //TODO
      //var response = OperationResultWrapper.CreateResponse(SearchUsersAsync, context.Message.Value);

      //await context.RespondAsync<IOperationResult<ISearchResponse>>(response);
    }
  }
}
