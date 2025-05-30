﻿using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Requests.Filtres;
using UniversityHelper.UserService.Models.Dto.Requests.User.Filters;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.UserService.Data.Interfaces;

[AutoInject]
public interface IUserRepository
{
  Task<DbUser> GetAsync(GetUserFilter filter, CancellationToken cancellationToken = default);

  Task<DbUser> GetAsync(Guid userId);

  Task<List<Guid>> AreExistingIdsAsync(List<Guid> usersIds);

  Task<(List<DbUser> dbUsers, int totalCount)> FindAsync(FindUsersFilter filter, List<Guid> userIds = null, CancellationToken cancellationToken = default);

  Task CreateAsync(DbUser dbUser);

  Task<bool> EditUserAdditionAsync(Guid userId, JsonPatchDocument<DbUserAddition> patch);

  Task<bool> EditUserAsync(Guid id, JsonPatchDocument<DbUser> userPatch);

  Task<bool> SwitchActiveStatusAsync(Guid userId, bool isActive);

  IQueryable<DbUser> SearchAsync(string searchText, IQueryable<DbUser> usersQuery = null);
}
