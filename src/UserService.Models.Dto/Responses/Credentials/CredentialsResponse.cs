﻿namespace UniversityHelper.UserService.Models.Dto.Responses.Credentials;

  public record CredentialsResponse
  {
      public Guid UserId { get; set; }
      public string AccessToken { get; set; }
      public string RefreshToken { get; set; }
      public double AccessTokenExpiresIn { get; set; }
      public double RefreshTokenExpiresIn { get; set; }
  }
