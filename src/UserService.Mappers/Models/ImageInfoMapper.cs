using HerzenHelper.Models.Broker.Models;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models
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
