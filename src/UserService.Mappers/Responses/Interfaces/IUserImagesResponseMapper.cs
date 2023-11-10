using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Responses.Image;
using System;
using System.Collections.Generic;

namespace UniversityHelper.UserService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IUserImagesResponseMapper
  {
    public UserImagesResponse Map(Guid userId, List<ImageInfo> images);
  }
}
