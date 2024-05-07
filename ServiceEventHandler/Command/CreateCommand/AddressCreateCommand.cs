using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.CreateCommand
{
    public class AddressCreateCommand
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Street { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Number { get; set; } = string.Empty;
        public string DetailAddress { get; set; } = string.Empty;
        public string? VenueName { get; set; } = string.Empty;
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string CityName { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string CountryName { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string StateName { get; set; }
    }
}