using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.ComponentModel.DataAnnotations;

namespace ServicesQueries.Dto
{
    public class CountryDto
    {
        public char CountryId { get; set; }
        public string Name { get; set; }

    }
}