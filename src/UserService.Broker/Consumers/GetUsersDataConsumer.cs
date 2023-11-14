using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.Core.RedisSupport.Configurations;
using UniversityHelper.Core.RedisSupport.Constants;
using UniversityHelper.Core.RedisSupport.Extensions;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.Models.Broker.Requests.User;
using UniversityHelper.Models.Broker.Responses.User;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Models.Dto.Requests.Filtres;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Consumers;

public class GetUsersDataConsumer : IConsumer<IGetUsersDataRequest>
{
  private readonly IUserRepository _userRepository;
  private readonly IOptions<RedisConfig> _redisConfig;
  private readonly IGlobalCacheRepository _globalCache;

  private async Task<List<UserData>> GetUserInfoAsync(IGetUsersDataRequest request)
  {
    (List<DbUser> dbUsers, int totalCount) =
      await _userRepository.FindAsync(
        filter: new FindUsersFilter() {
          TakeCount = int.MaxValue,
          IncludeCurrentAvatar = true,
          //IncludeCommunications = request.IncludeBaseEmail
        }, //TODO fix takeCount
        userIds: request.UsersIds);

    return dbUsers.Select(
      u => new UserData(
        id: u.Id,
        imageId: u.Avatars?.FirstOrDefault()?.AvatarId,
        firstName: u.FirstName,
        middleName: u.MiddleName,
        lastName: u.LastName,
        isActive: u.IsActive,
        email: request.IncludeBaseEmail
          ? u.Communications.FirstOrDefault(c => c.Type == (int)CommunicationType.BaseEmail)?.Value
          : null
          ))
      .ToList();
  }

  public GetUsersDataConsumer(
    IUserRepository userRepository,
    IOptions<RedisConfig> redisConfig,
    IGlobalCacheRepository globalCache)
  {
    _userRepository = userRepository;
    _redisConfig = redisConfig;
    _globalCache = globalCache;
  }

  public async Task Consume(ConsumeContext<IGetUsersDataRequest> context)
  {
    List<UserData> users = await GetUserInfoAsync(context.Message);

    await context.RespondAsync<IOperationResult<IGetUsersDataResponse>>(
      OperationResultWrapper.CreateResponse((_) => IGetUsersDataResponse.CreateObj(users), context));

    if (users is not null)
    {
      await _globalCache.CreateAsync(
        Cache.Users,
        users.Select(u => u.Id).GetRedisCacheKey(nameof(IGetUsersDataRequest), context.Message.GetBasicProperties()),
        users,
        users.Select(u => u.Id).ToList(),
        TimeSpan.FromMinutes(_redisConfig.Value.CacheLiveInMinutes));
    }
  }
}
