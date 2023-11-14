using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.UserService.Models.Dto.Requests.Credentials;

public record CreateCredentialsRequest
{
  public Guid UserId { get; set; }
  [Required]
  public string Login { get; set; }
  [Required]
  public string Password { get; set; }
}
