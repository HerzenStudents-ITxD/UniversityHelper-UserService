using HerzenHelper.Core.BrokerSupport.Broker;
using HerzenHelper.Core.RedisSupport.Configurations;
using HerzenHelper.Core.RedisSupport.Constants;
using HerzenHelper.Core.RedisSupport.Extensions;
using HerzenHelper.Core.RedisSupport.Helpers.Interfaces;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.Models.Broker.Models.User;
using HerzenHelper.Models.Broker.Requests.User;
using HerzenHelper.Models.Broker.Responses.User;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Enums;
using HerzenHelper.UserService.Models.Dto.Requests.Filtres;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Broker.Consumers
{
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
}
