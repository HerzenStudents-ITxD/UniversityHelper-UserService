// using UniversityHelper.Core.BrokerSupport.Broker;
// using UniversityHelper.Core.RedisSupport.Configurations;
// using UniversityHelper.Core.RedisSupport.Constants;
// using UniversityHelper.Core.RedisSupport.Extensions;
// using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
// using UniversityHelper.Models.Broker.Models;
// using UniversityHelper.Models.Broker.Requests.User;
// using UniversityHelper.Models.Broker.Responses.User;
// using UniversityHelper.UserService.Data.Interfaces;
// using UniversityHelper.UserService.Models.Db;
// using UniversityHelper.UserService.Models.Dto.Requests.Filtres;
// using MassTransit;
// using Microsoft.Extensions.Options;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace UniversityHelper.UserService.Broker.Consumers
// {
//   public class FilterUsersDataConsumer : IConsumer<IFilteredUsersDataRequest>
//   {
//     private readonly IUserRepository _userRepository;
//     private readonly IOptions<RedisConfig> _redisConfig;
//     private readonly IGlobalCacheRepository _globalCache;

//     private async Task<(List<UserData> userData, int totalCount)> GetUserInfoAsync(IFilteredUsersDataRequest request)
//     {
//       List<DbUser> dbUsers = new();

//       int totalCount = 0;

//       if (request.SkipCount > -1 && request.TakeCount > 0)
//       {
//         (dbUsers, totalCount) = await _userRepository.FindAsync(new FindUsersFilter()
//         {
//           SkipCount = request.SkipCount,
//           TakeCount = request.TakeCount,
//           IncludeCurrentAvatar = true,
//           IsActive = request.IsActive,
//           IsAscendingSort = request.AscendingSort,
//           FullNameIncludeSubstring = request.FullNameIncludeSubstring
//         },
//         request.UsersIds);
//       }

//       return (dbUsers.Select(
//         u => new UserData(
//           id: u.Id,
//           imageId: u.Avatars?.FirstOrDefault(ua => ua.IsCurrentAvatar)?.AvatarId,
//           firstName: u.FirstName,
//           middleName: u.MiddleName,
//           lastName: u.LastName,
//           isActive: u.IsActive))
//         .ToList(),
//         totalCount);
//     }

//     public FilterUsersDataConsumer(
//       IUserRepository userRepository,
//       IOptions<RedisConfig> redisConfig,
//       IGlobalCacheRepository globalCache)
//     {
//       _userRepository = userRepository;
//       _redisConfig = redisConfig;
//       _globalCache = globalCache;
//     }

//     public async Task Consume(ConsumeContext<IFilteredUsersDataRequest> context)
//     {
//       (List<UserData> users, int usersCount) = await GetUserInfoAsync(context.Message);

//       await context.RespondAsync<IOperationResult<IFilteredUsersDataResponse>>(
//         OperationResultWrapper.CreateResponse((_) => IFilteredUsersDataResponse.CreateObj(users, usersCount), context));

//       if (users is not null)
//       {
//         await _globalCache.CreateAsync(
//           Cache.Users,
//           context.Message.UsersIds.GetRedisCacheKey(nameof(IFilteredUsersDataRequest), context.Message.GetBasicProperties()),
//           (users, usersCount),
//           users.Select(u => u.Id).ToList(),
//           TimeSpan.FromMinutes(_redisConfig.Value.CacheLiveInMinutes));
//       }
//     }
//   }
// }