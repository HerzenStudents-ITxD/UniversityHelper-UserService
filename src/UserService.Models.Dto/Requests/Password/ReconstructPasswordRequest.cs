using System;
using System.ComponentModel.DataAnnotations;

namespace HerzenHelper.UserService.Models.Dto
{
  public record ReconstructPasswordRequest
  //password and secred must be receiven from body! receiving from query is not secure
  {
    public Guid UserId { get; set; }

    [Required]
    public string Secret { get; set; }

    [Required]
    public string NewPassword { get; set; }
  }
}