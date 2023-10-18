using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Models.Broker.Models;
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

      return ISearchResponse.CreateObj(
        users.Select(
          u => new SearchInfo(u.Id, string.Join(" ", u.LastName, u.FirstName))).ToList());
    }

    public SearchUsersConsumer(
      IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<ISearchUsersRequest> context)
    {
      var response = OperationResultWrapper.CreateResponse(SearchUsersAsync, context.Message.Value);

      await context.RespondAsync<IOperationResult<ISearchResponse>>(response);
    }
  }
}
