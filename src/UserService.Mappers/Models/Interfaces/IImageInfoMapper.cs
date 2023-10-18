using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IImageInfoMapper
  {
    ImageInfo Map(ImageData image);
  }
}
