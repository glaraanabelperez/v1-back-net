

using Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.CreateCommand
{
    public class ProfileDancerCreateCommand
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public int DanceLevelId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public int DanceRolId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public int UserId { get; set; }
        public double? Height { get; set; }
    }
}