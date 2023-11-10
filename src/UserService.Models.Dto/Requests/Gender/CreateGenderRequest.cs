using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.UserService.Models.Dto.Requests.Gender
{
  public class CreateGenderRequest
  {
    [Required]
    public string Name { get; set; }
  }
}
