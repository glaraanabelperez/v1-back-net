using Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.UpdateCommand
{
    public class EventUpdateCommand
    {
        public int EventId { get; set; }
        public int? UserIdCreator { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? AddressId { get; set; }
        public DateTime? DateInit { get; set; }
        public DateTime? DateFinish { get; set; }
        public int? EventStateId { get; set; }
        public int? TypeEventId { get; set; }
        public int? RolId { get; set; }
        public int? LevelId { get; set; }
        public int? Cupo { get; set; }
        public bool? Couple { get; set; }
        public int? CycleId { get; set; }

        public AddressUpdateCommand? Address { get; set; }

    }


}