using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.Image;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models
{
  public class ImageInfoMapper : IImageInfoMapper
  {
    public ImageInfo Map(ImageData image)
    {
      return image is null
        ? default
        : new ImageInfo
        {
          Id = image.ImageId,
          ParentId = image.ParentId,
          Content = image.Content,
          Extension = image.Extension,
          Name = image.Name
        };
    }
  }
}
