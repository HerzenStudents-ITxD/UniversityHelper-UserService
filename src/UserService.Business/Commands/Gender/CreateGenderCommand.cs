using FluentValidation.Results;
using HerzenHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using HerzenHelper.Core.Extensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.UserService.Business.Commands.User.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Db.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Requests.Gender;
using HerzenHelper.UserService.Validation.Gender.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.User
{
  public class CreateGenderCommand : ICreateGenderCommand
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbGenderMapper _mapper;
    private readonly IGenderRepository _genderRepository;
    private readonly ICreateGenderRequestValidator _validator;
    private readonly IResponseCreator _responseCreator;
    private readonly IAccessValidator _accessValidator;

    public CreateGenderCommand(
      IHttpContextAccessor httpContextAccessor,
      IDbGenderMapper mapper,
      IGenderRepository genderRepository,
      ICreateGenderRequestValidator validator,
      IResponseCreator responseCreator,
      IAccessValidator accessValidator)
    {
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _genderRepository = genderRepository;
      _validator = validator;
      _responseCreator = responseCreator;
      _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateGenderRequest request)
    {
      if (!await _accessValidator.IsAdminAsync(_httpContextAccessor.HttpContext.GetUserId()))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      DbGender dbGender = _mapper.Map(request);
      await _genderRepository.CreateAsync(dbGender);

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return new OperationResultResponse<Guid?>(
        body: dbGender.Id);
    }
  }
}
