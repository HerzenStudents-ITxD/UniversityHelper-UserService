﻿using FluentValidation;
using UniversityHelper.UserService.Models.Dto;
using UniversityHelper.UserService.Models.Dto.Enums;
using UniversityHelper.UserService.Validation.Communication.Interfaces;
using UniversityHelper.UserService.Validation.Image.Interfaces;
using UniversityHelper.UserService.Validation.Password.Interfaces;
using UniversityHelper.UserService.Validation.User.Interfaces;
using System.Text.RegularExpressions;

namespace UniversityHelper.UserService.Validation.User;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>, ICreateUserRequestValidator
{
  private static Regex NumberRegex = new(@"\d");
  private static Regex SpecialCharactersRegex = new(@"[$&+,:;=?@#|<>.^*()%!]");
  private static Regex NameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+)$");

  public CreateUserRequestValidator(
    ICreateCommunicationRequestValidator communicationValidator,
    ICreateAvatarRequestValidator imageValidator,
    IPasswordValidator passwordValidator)
  {
    RuleFor(user => user.FirstName)
      .Must(x => !NumberRegex.IsMatch(x))
      .WithMessage("First name must not contain numbers.")
      .Must(x => !SpecialCharactersRegex.IsMatch(x))
      .WithMessage("First name must not contain special characters.")
      .MaximumLength(45).WithMessage("First name is too long.")
      .Must(x => NameRegex.IsMatch(x.Trim()))
      .WithMessage("First name contains invalid characters.");

    RuleFor(user => user.LastName)
      .Must(x => !NumberRegex.IsMatch(x))
      .WithMessage("Last name must not contain numbers.")
      .Must(x => !SpecialCharactersRegex.IsMatch(x))
      .WithMessage("Last name must not contain special characters.")
      .MaximumLength(45).WithMessage("Last name is too long.")
      .Must(x => NameRegex.IsMatch(x.Trim()))
      .WithMessage("Last name contains invalid characters.");

    When(
      user => !string.IsNullOrEmpty(user.MiddleName),
      () =>
        RuleFor(user => user.MiddleName)
          .Must(x => !NumberRegex.IsMatch(x))
          .WithMessage("Middle name must not contain numbers.")
          .Must(x => !SpecialCharactersRegex.IsMatch(x))
          .WithMessage("Middle name must not contain special characters.")
          .MaximumLength(45).WithMessage("Middle name is too long.")
          .Must(x => NameRegex.IsMatch(x.Trim()))
          .WithMessage("Middle name contains invalid characters."));

    When(
      user => (user.AvatarImage != null),
      () =>
        RuleFor(user => user.AvatarImage)
          .SetValidator(imageValidator)
      );

    When(user => user.About is not null, () =>
    {
      RuleFor(user => user.About)
        .MaximumLength(150).WithMessage("About is too long.");
    });

    RuleFor(user => user.Communication)
      .Cascade(CascadeMode.Stop)
      .Must(x => x.Type == CommunicationType.Email)
      .WithMessage("Communication type must be email.")
      .SetValidator(communicationValidator);

    When(user => user.UserUniversity is not null && user.UserUniversity.Rate is not null, () =>
    {
      RuleFor(user => user.UserUniversity.Rate)
        .GreaterThan(0)
        .LessThanOrEqualTo(1);

      //RuleFor(user => user.UserUniversity.ContractTermType)
      //  .Must(x => Enum.IsDefined(typeof(ContractTerm), x))
      //  .WithMessage("Wrong contract term type.");
    });

    When(
      user => (!string.IsNullOrEmpty(user.Password)),
      () =>
        RuleFor(user => user.Password)
          .SetValidator(passwordValidator)
      );
  }
}
