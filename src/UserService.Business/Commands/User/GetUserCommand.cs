﻿using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.Company;
using UniversityHelper.Models.Broker.Models.Department;
using UniversityHelper.Models.Broker.Models.Office;
using UniversityHelper.Models.Broker.Models.Position;
using UniversityHelper.Models.Broker.Models.Right;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Business.Interfaces;
using UniversityHelper.UserService.Data.Interfaces;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Mappers.Responses.Interfaces;
using UniversityHelper.UserService.Models.Db;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.User.Filters;
using UniversityHelper.UserService.Models.Dto.Responses.User;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Business.Commands.User;

public class GetUserCommand : IGetUserCommand
{
  private readonly IUserRepository _repository;
  private readonly IUserResponseMapper _mapper;
  private readonly IOfficeInfoMapper _officeMapper;
  private readonly IDepartmentInfoMapper _departmentMapper;
  private readonly ICompanyUserInfoMapper _companyUserMapper;
  private readonly IPositionInfoMapper _positionMapper;
  private readonly IRoleInfoMapper _roleMapper;
  private readonly ICompanyService _companyService;
  private readonly IDepartmentService _departmentService;
  private readonly IImageService _imageService;
  private readonly IOfficeService _officeService;
  private readonly IPositionService _positionService;
  private readonly IRightService _rightService;
  private readonly IResponseCreator _responseCreator;

  public GetUserCommand(
    IUserRepository repository,
    IUserResponseMapper mapper,
    IRoleInfoMapper roleMapper,
    IDepartmentInfoMapper departmentMapper,
    ICompanyUserInfoMapper companyUserMapper,
    IPositionInfoMapper positionMapper,
    IOfficeInfoMapper officeMapper,
    ICompanyService companyService,
    IDepartmentService departmentService,
    IImageService imageService,
    IOfficeService officeService,
    IPositionService positionService,
    IRightService rightService,
    IResponseCreator responseCreator)
  {
    _repository = repository;
    _mapper = mapper;
    _roleMapper = roleMapper;
    _departmentMapper = departmentMapper;
    _companyUserMapper = companyUserMapper;
    _positionMapper = positionMapper;
    _officeMapper = officeMapper;
    _companyService = companyService;
    _departmentService = departmentService;
    _imageService = imageService;
    _officeService = officeService;
    _positionService = positionService;
    _rightService = rightService;
    _responseCreator = responseCreator;
  }

  public async Task<OperationResultResponse<UserResponse>> ExecuteAsync(GetUserFilter filter, CancellationToken cancellationToken = default)
  {
    OperationResultResponse<UserResponse> response = new();

    if (filter is null ||
      (filter.UserId is null &&
        string.IsNullOrEmpty(filter.Email)))
    {
      return _responseCreator.CreateFailureResponse<UserResponse>(
        HttpStatusCode.BadRequest,
        new List<string> { "You must specify 'userId' or|and 'email'." });
    }

    DbUser dbUser = await _repository.GetAsync(filter: filter);

    if (dbUser is null)
    {
      return _responseCreator.CreateFailureResponse<UserResponse>(
        HttpStatusCode.NotFound);
    }

    Task<List<CompanyData>> companiesTask = filter.IncludeCompany
      ? _companyService.GetCompaniesAsync(dbUser.Id, response.Errors, cancellationToken)
      : Task.FromResult(null as List<CompanyData>);

    Task<List<DepartmentData>> departmentsTask = filter.IncludeDepartment
      ? _departmentService.GetDepartmentsAsync(
          userId: dbUser.Id, errors: response.Errors, includeChildDepartmentsIds: true, cancellationToken: cancellationToken)
      : Task.FromResult(null as List<DepartmentData>);

    Task<List<ImageInfo>> imagesTask = filter.IncludeAvatars || filter.IncludeCurrentAvatar
      ? _imageService.GetImagesAsync(dbUser.Avatars?.Select(ua => ua.AvatarId).ToList(), response.Errors, cancellationToken)
      : Task.FromResult(null as List<ImageInfo>);

    Task<List<OfficeData>> officesTask = filter.IncludeOffice
      ? _officeService.GetOfficesAsync(dbUser.Id, response.Errors, cancellationToken)
      : Task.FromResult(null as List<OfficeData>);

    Task<List<PositionData>> positionsTask = filter.IncludePosition
      ? _positionService.GetPositionsAsync(dbUser.Id, response.Errors, cancellationToken)
      : Task.FromResult(null as List<PositionData>);

    Task<List<RoleData>> rolesTask = filter.IncludeRole
      ? _rightService.GetRolesAsync(dbUser.Id, filter.Locale, response.Errors, cancellationToken)
      : Task.FromResult(null as List<RoleData>);

    List<CompanyData> companies = await companiesTask;
    List<DepartmentData> departments = await departmentsTask;
    List<ImageInfo> images = await imagesTask;
    List<OfficeData> offices = await officesTask;
    List<PositionData> positions = await positionsTask;
    List<RoleData> roles = await rolesTask;

    response.Body = _mapper.Map(
      dbUser,
      _companyUserMapper.Map(companies?.FirstOrDefault(), companies?.FirstOrDefault()?.Users.FirstOrDefault(cu => cu.UserId == dbUser.Id)),
      images?.FirstOrDefault(i => i.Id == dbUser.Avatars.FirstOrDefault(ua => ua.IsCurrentAvatar).AvatarId),
      _departmentMapper.Map(dbUser.Id, departments?.FirstOrDefault()),
      images,
      _officeMapper.Map(offices?.FirstOrDefault()),
      _positionMapper.Map(positions?.FirstOrDefault()),
      _roleMapper.Map(roles?.FirstOrDefault()));

    return response;
  }
}
