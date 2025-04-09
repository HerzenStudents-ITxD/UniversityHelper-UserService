//using UniversityHelper.Models.Broker.Models.University;
//using UniversityHelper.UserService.Mappers.Models.Interfaces;
//using UniversityHelper.UserService.Models.Dto.Models;

//namespace UniversityHelper.UserService.Mappers.Models;

//public class UniversityUserInfoMapper : IUniversityUserInfoMapper
//{
//  public UniversityUserInfo Map(UniversityData universityData, UniversityUserData universityUserData)
//  {
//    return universityData is null || universityUserData is null
//      ? default
//      : new UniversityUserInfo
//      {
//        University = new UniversityInfo
//        {
//          Id = universityData.Id,
//          Name = universityData.Name
//        },
//        //ContractSubject = universityUserData.ContractSubject,
//        //ContractTermType = universityUserData.ContractTermType,
//        Rate = universityUserData.Rate,
//        //StartWorkingAt = universityUserData.StartWorkingAt,
//        //EndWorkingAt = universityUserData.EndWorkingAt,
//        //Probation = universityUserData.Probation
//      };
//  }
//}
