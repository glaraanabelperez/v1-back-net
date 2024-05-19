using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Command.UpdateCommand
{
    public class AddressUpdateCommand
    {
        public int? CityId { get; set; }
        [MaxLength(255)]
        public string? Street { get; set; }
        [MaxLength(255)]
        public string? Number { get; set; } = string.Empty;
        public string? DetailAddress { get; set; } = string.Empty;
        public string? VenueName { get; set; } = string.Empty;
       
    }
}