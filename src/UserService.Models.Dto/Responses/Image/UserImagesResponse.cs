using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Models.Dto.Responses.Image;

public record UserImagesResponse
{
  public Guid UserId { get; set; }
  public List<ImageInfo> Images { get; set; }
}
