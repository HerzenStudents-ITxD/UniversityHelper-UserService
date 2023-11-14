using UniversityHelper.UserService.Mappers.Responses.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Responses.Image;
using System;
using System.Collections.Generic;

namespace UniversityHelper.UserService.Mappers.Responses;

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
