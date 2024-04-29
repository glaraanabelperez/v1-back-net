using Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.CreateCommand
{
    public class EventUpdateCommand
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de AplicationId debe ser mayor que cero.")]
        public int EventId { get; set; }
        public int? UserIdCreator { get; set; }
        [MaxLength(250)]
        public string? Name { get; set; }
        [MaxLength(250)]
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


    }
}