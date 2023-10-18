using System.ComponentModel.DataAnnotations;

namespace HerzenHelper.UserService.Models.Dto.Requests.Gender
{
  public class CreateGenderRequest
  {
    [Required]
    public string Name { get; set; }
  }
}
