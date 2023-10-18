using System;

namespace HerzenHelper.UserService.Models.Dto.Models
{
    public record PositionInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
