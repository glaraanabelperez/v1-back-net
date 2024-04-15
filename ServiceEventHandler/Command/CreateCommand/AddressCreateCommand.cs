using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.CreateCommand
{
    public class AddressCreateCommand
    {
        /// <summary>
        /// User relationship with Address, in Case be a new register to Address-
        /// </summary>
        public int? UserId { get; set; }
        public int? CityId { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Street { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Number { get; set; } = string.Empty;
        public string DetailAddress { get; set; } = string.Empty;
        public bool StateAddress { get; set; } = true;

        public CityCreateCommand? city { get; set; }

    }
}