using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.Right;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IRightService
{
  Task<List<RoleData>> GetRolesAsync(
    Guid userId,
    string locale,
    List<string> errors,
    CancellationToken cancellationToken = default);
}
