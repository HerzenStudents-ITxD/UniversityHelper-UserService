using HerzenHelper.UserService.Mappers.Responses.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;
using HerzenHelper.UserService.Models.Dto.Responses.Image;
using System;
using System.Collections.Generic;

namespace HerzenHelper.UserService.Mappers.Responses
{
  public class UserImagesResponseMapper : IUserImagesResponseMapper
  {
    public UserImagesResponse Map(Guid userId, List<ImageInfo> images)
    {
      return images is null
        ? default
        : new UserImagesResponse
        {
          UserId = userId,
          Images = images
        };
    }
  }
}
