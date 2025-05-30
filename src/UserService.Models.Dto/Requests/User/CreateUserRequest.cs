﻿using UniversityHelper.UserService.Models.Dto.Requests.Avatar;
using UniversityHelper.UserService.Models.Dto.Requests.Communication;
using UniversityHelper.UserService.Models.Dto.Requests.UserUniversity;
using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.UserService.Models.Dto;

public record CreateUserRequest
{
  [Required]
  public string FirstName { get; set; }
  [Required]
  public string LastName { get; set; }
  public string MiddleName { get; set; }
  public bool IsAdmin { get; set; } = false;
  public string About { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public Guid? DepartmentId { get; set; }
  public Guid? OfficeId { get; set; }
  public Guid? PositionId { get; set; }
  public Guid? RoleId { get; set; }
  public string Password { get; set; }
  public CreateUserUniversityRequest UserUniversity { get; set; }
  public CreateAvatarRequest AvatarImage { get; set; }
  [Required]
  public CreateCommunicationRequest Communication { get; set; }
}