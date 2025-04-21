// using FluentValidation;
// using UniversityHelper.UserService.Data.Interfaces;
// using UniversityHelper.UserService.Models.Db;
// using UniversityHelper.UserService.Models.Dto;
// using UniversityHelper.UserService.Models.Dto.Enums;
// using UniversityHelper.UserService.Models.Dto.Requests.User;
// using UniversityHelper.UserService.Validation.Communication.Interfaces;
// using UniversityHelper.UserService.Validation.Image.Interfaces;
// using UniversityHelper.UserService.Validation.Password.Interfaces;
// using UniversityHelper.UserService.Validation.User.Interfaces;
// using System.Text.RegularExpressions;
// using UniversityHelper.Core.Validators;

// namespace UniversityHelper.UserService.Validation.User;

// public class EditUserActiveRequestValidator : AbstractValidator<(DbUser dbUser, EditUserActiveRequest request)>, IEditUserActiveRequestValidator
// {
//     public EditUserActiveRequestValidator(
//       IPendingUserRepository _pendingRepository)
//     {
//       RuleFor(x => x)
//         .Cascade(CascadeMode.Stop)
//         .Must(x => x.dbUser.IsActive != x.request.IsActive)
//         .WithMessage("Error is active value.")
//         .MustAsync(async (x, _) =>
//           !(x.request.IsActive && await _pendingRepository.GetAsync(x.request.UserId) is not null))
//         .WithMessage("User already pending.")
//         .Must(x =>
//         {
//           if (x.request.IsActive && x.request.CommunicationId.HasValue)
//           {
//             DbUserCommunication uc = x.dbUser.Communications.FirstOrDefault(c => c.Id == x.request.CommunicationId);
//             return uc is not null && (uc.Type == (int)CommunicationType.Email || uc.Type == (int)CommunicationType.BaseEmail);
//           }

//           return !x.request.IsActive;
//         })
//         .WithMessage("Wrong user communication.");
//     }
//   }
