using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.CreateCommand
{
    public class InscriptionCommandCreate
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor  del usuario debe ser mayor que cero.")]
        public int UserId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor del evento debe ser mayor que cero.")]
        public int EventId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "El valor del rol debe ser mayor que cero.")]
        public int ProfileDancerId { get; set; }

        [Required]
        public bool InscripcionWithCouple { get; set; }

    }
}