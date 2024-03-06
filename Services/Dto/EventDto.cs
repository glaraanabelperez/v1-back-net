using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.ComponentModel.DataAnnotations;

namespace Abrazos.Services.Dto
{
    public class EventDto 
    {
        public int EventId { get; set; }
        public int UserIdCreator_FK { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int AddressId_fk { get; set; }
        public string? Image { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateFinish { get; set; }
        public int EventStateId_fk { get; set; }
        public int TypeEventId_fk { get; set; }

        public int Cupo { get; set; }
        public int RolId { get; set; }
        public int LevelId { get; set; }


        public DanceLevelDto Level { get; set; }
        public DanceRolDto Rol { get; set; }

        public EventStateDto EventState { get; set; }
        public TypeEventDto TypeEvent { get; set; }
        public UserDto UserCreator { get; set; }
        public AddressDto Address { get; set; }
        public ICollection<CouplesEvent_DateDto> CouplesEvents { get; set; } = new List<CouplesEvent_DateDto>();
        public ICollection<CycleEventDto>? CycleEvents { get; set; }




    }
}