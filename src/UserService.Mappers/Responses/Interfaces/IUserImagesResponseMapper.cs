using HerzenHelper.Core.Attributes;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Responses.Image;
using System;
using System.Collections.Generic;

namespace HerzenHelper.UserService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IUserImagesResponseMapper
  {
    public UserImagesResponse Map(Guid userId, List<ImageInfo> images);
  }
}
