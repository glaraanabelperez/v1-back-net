using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserEventInscription
    {
        public int UserEventInscriptionId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int ProfileDancerId { get; set; }
        public bool Partner { get; set; }
        public bool State { get; set; }
        public ProfileDancer Profile { get; set; }
        public Event Event { get; set; }
        public User User { get; set; }


    }
}