﻿using UniversityHelper.Core.Attributes;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.Avatar;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityHelper.UserService.Broker.Requests.Interfaces;

[AutoInject]
public interface IImageService
{
  Task<List<ImageInfo>> GetImagesAsync(List<Guid> imageIds, List<string> errors, CancellationToken cancellationToken = default);

  Task<Guid?> CreateImageAsync(CreateAvatarRequest request, List<string> errors);
}
