using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class EventStateDto
    {
        public int EventStateId { get; set; }
        public string Name { get; set; }

    }
}