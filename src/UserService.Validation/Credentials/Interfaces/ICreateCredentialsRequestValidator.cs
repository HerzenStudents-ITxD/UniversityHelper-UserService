using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto.Requests.Credentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Validation.Credentials.Interfaces
{
    [AutoInject]
    public interface ICreateCredentialsRequestValidator : IValidator<CreateCredentialsRequest>
    {
    }
}
