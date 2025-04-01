using UniversityHelper.UserService.Models.Dto.Enums;
using System;

namespace UniversityHelper.UserService.Models.Dto.Requests.User;

public class EditUserRequest
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string MiddleName { get; set; }
  public UserStatus Status { get; set; }
  public bool IsAdmin { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public string About { get; set; }

}