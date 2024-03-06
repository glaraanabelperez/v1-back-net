using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CycleDto
    {
        public int CycleId { get; set; }
        public string CycleTitle { get; set; }
        public string Description { get; set; }


    }
}