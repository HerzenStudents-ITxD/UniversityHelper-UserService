using UniversityHelper.Models.Broker.Requests.User;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace UniversityHelper.UserService.Broker.Consumers;

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
