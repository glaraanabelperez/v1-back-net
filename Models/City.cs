using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }

        public ICollection<Address>? Address { get; set; } = new List<Address>();

    }
}