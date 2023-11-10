using UniversityHelper.Core.Enums;
using UniversityHelper.Core.FluentValidationExtensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Core.Validators.Interfaces;
using UniversityHelper.UserService.Business.Commands.Gender.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.Gender.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.Gender
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
