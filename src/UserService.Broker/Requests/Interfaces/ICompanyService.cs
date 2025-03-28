﻿using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Company;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface ICompanyService
{
  Task<List<CompanyData>> GetCompaniesAsync(Guid userId, List<string> errors, CancellationToken cancellationToken = default);
}
