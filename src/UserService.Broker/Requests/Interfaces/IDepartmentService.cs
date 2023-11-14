using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Department;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IDepartmentService
{
  Task<List<DepartmentData>> GetDepartmentsAsync(
    Guid userId, List<string> errors, bool includeChildDepartmentsIds = false, CancellationToken cancellationToken = default);
}
