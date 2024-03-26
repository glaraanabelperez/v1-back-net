using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.ComponentModel.DataAnnotations;

namespace ServicesQueries.Dto
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; } 

        public ICollection<AddressDto>? Address { get; set; } 

    }
}