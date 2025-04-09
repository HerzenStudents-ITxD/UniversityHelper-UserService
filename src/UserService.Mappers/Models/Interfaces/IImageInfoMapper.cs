using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Image;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces;

[AutoInject]
public interface IImageInfoMapper
{
  ImageInfo Map(ImageData image);
}
