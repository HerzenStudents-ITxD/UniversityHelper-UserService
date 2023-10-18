using HerzenHelper.Models.Broker.Models.Office;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models
{
  public class OfficeInfoMapper : IOfficeInfoMapper
  {
    public OfficeInfo Map(OfficeData office)
    {
      return office is null
        ? default
        : new OfficeInfo
        {
          Id = office.Id,
          Name = office.Name,
          Address = office.Address,
          City = office.City,
          Longitude = office.Longitude,
          Latitude = office.Latitude
        };
    }
  }
}
