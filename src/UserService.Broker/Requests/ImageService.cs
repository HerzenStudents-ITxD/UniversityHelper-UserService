﻿using UniversityHelper.Models.Broker.Requests.Image;
using UniversityHelper.UserService.Broker.Requests.Interfaces;
using UniversityHelper.UserService.Mappers.Models.Interfaces;
using UniversityHelper.UserService.Models.Dto.Models;
using UniversityHelper.UserService.Models.Dto.Requests.Avatar;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UniversityHelper.UserService.Broker.Requests;

public class ImageService : IImageService
{
  private readonly ILogger<ImageService> _logger;
  private readonly IRequestClient<IGetImagesRequest> _rcGetImages;
  private readonly IRequestClient<ICreateImagesRequest> _rcCreateImages;
  private readonly IImageInfoMapper _mapper;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public ImageService(
    ILogger<ImageService> logger,
    IRequestClient<IGetImagesRequest> rcGetImages,
    IRequestClient<ICreateImagesRequest> rcCreateImages,
    IImageInfoMapper mapper,
    IHttpContextAccessor httpContextAccessor)
  {
    _logger = logger;
    _rcCreateImages = rcCreateImages;
    _rcGetImages = rcGetImages;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<List<ImageInfo>> GetImagesAsync(List<Guid> imagesIds, List<string> errors, CancellationToken cancellationToken = default)
  {
    return new();
    //return imagesIds is null || !imagesIds.Any()
    //  ? default
    //  : (await RequestHandler.ProcessRequest<IGetImagesRequest, IGetImagesResponse>(
    //      _rcGetImages,
    //      IGetImagesRequest.CreateObj(imagesIds, ImageSource.User),
    //      errors,
    //      _logger))
    //    ?.ImagesData
    //    .Select(_mapper.Map).ToList();
  }

  public async Task<Guid?> CreateImageAsync(CreateAvatarRequest request, List<string> errors)
  {
    return new();
    //return request is null
    //  ? null
    //  : (await _rcCreateImages.ProcessRequest<ICreateImagesRequest, ICreateImagesResponse>(
    //      ICreateImagesRequest.CreateObj(
    //        new() { new CreateImageData(request.Name, request.Content, request.Extension) },
    //        ImageSource.User,
    //        _httpContextAccessor.HttpContext.GetUserId()),
    //      errors,
    //      _logger))
    //    ?.ImagesIds?.FirstOrDefault();
  }
}
