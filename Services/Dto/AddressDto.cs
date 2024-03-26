using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ServicesQueries.Dto
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }


        public string Street { get; set; }
        public string Number { get; set; }
        public string DetailAddress { get; set; }
        public bool StateAddress { get; set; }


        public int CountryId { get; set; }
        public string CountryName { get; set; }



    }
}