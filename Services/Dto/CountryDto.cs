using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ServicesQueries.Dto
{
    public class CountryDto
    {
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;

        //public ICollection<City>? Cities { get; set; } = null;

    }
}