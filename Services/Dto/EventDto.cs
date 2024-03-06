using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.ComponentModel.DataAnnotations;

namespace Abrazos.Services.Dto
{
    public class EventDto 
    {
        public int EventId { get; set; }
        public int UserIdCreator_FK { get; set; }
        public string UserCreatorName { get; set; } = null!;
        public string? UserCreatorLastName { get; set; }
        public string EventName { get; set; } = null!;
        public string? EventDescription { get; set; }
        public int AddressId_fk { get; set; }
        public string? Image { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateFinish { get; set; }
        public int EventStateId_fk { get; set; }
        public string EventStateName { get; set; }
        public int TypeEventId_fk { get; set; }
        public string TypeEventName { get; set; }

        public int Cupo { get; set; }
        public int RolId { get; set; }
        public string RolName { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public int CycleId { get; set; }
        public string CycleTitle { get; set; }
        public string CycleDescription { get; set; }

        public AddressDto Address { get; set; }




    }
}