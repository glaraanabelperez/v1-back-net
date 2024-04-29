using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.CreateCommand
{
    public class EventCreateCommand
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de AplicationId debe ser mayor que cero.")]
        public int UserIdCreator { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }
        public string? Image { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de la direccion debe ser mayor que cero.")]
        public int AddressId { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateFinish { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de EventStateId debe ser mayor que cero.")]
        public int EventStateId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de TypeEventId debe ser mayor que cero.")]
        public int TypeEventId{ get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de RolId debe ser mayor que cero.")]
        public int RolId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de Level debe ser mayor que cero.")]
        public int LevelId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor de Cupo debe ser mayor que cero.")]
        public int Cupo { get; set; }
        [Required]
        public bool Couple { get; set; }
        public int? CycleId { get; set; }

        public AddressCreateCommand? Address { get; set; }
        public CycleCommandCreate? Cycle { get; set; }

    }
}