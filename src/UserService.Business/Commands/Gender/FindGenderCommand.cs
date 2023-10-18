using HerzenHelper.Core.Enums;
using HerzenHelper.Core.FluentValidationExtensions;
using HerzenHelper.Core.Helpers.Interfaces;
using HerzenHelper.Core.Responses;
using HerzenHelper.Core.Validators.Interfaces;
using HerzenHelper.UserService.Business.Commands.Gender.Interfaces;
using HerzenHelper.UserService.Data.Interfaces;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Db;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Requests.Gender.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HerzenHelper.UserService.Business.Commands.Gender
{
  public class FindGenderCommand : IFindGenderCommand
  {
    private readonly IGenderRepository _genderRepository;
    private readonly IGenderInfoMapper _mapper;
    private readonly IResponseCreator _responseCreator;
    public FindGenderCommand(
      IGenderRepository genderRepository,
      IGenderInfoMapper mapper,
      IResponseCreator responseCreator)
    {
      _responseCreator = responseCreator;
      _genderRepository = genderRepository;
      _mapper = mapper;
    }
    public async Task<FindResultResponse<GenderInfo>> ExecuteAsync(FindGendersFilter filter)
    {
      //TODO
      //if (!_baseFindValidator.ValidateCustom(filter, out List<string> errors))
      //{
      //  return _responseCreator.CreateFailureFindResponse<GenderInfo>(HttpStatusCode.BadRequest, errors);
      //}

      FindResultResponse<GenderInfo> response = new();

      (List<DbGender> dbGenders, int totalCount) = await _genderRepository.FindGendersAsync(filter);

      response.TotalCount = totalCount;

      response.Body = _mapper.Map(dbGenders);

      return response;
    }
  }
}
