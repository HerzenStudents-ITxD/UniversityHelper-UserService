using FluentValidation;
using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto.Requests.Credentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Validation.Credentials.Interfaces
{
    [AutoInject]
    public interface ICreateCredentialsRequestValidator : IValidator<CreateCredentialsRequest>
    {
    }
}
