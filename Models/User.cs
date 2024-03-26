using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Models
{
    public class User 
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Pass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Celphone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? AvatarImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UserState { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ICollection<Address>? Address { get; set; } = new List<Address>();
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Image>? Images { get; set; } = new List<Image>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<UserPermission>? UserPermissions { get; set; } = new List<UserPermission>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ProfileDancer>? ProfileDancer { get; set; } = new List<ProfileDancer>();
        /// <summary>
        /// 
        /// </summary>
        public ICollection<CouplesEventDate>? CouplesEventsUserHost { get; set; } = new List<CouplesEventDate>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<CouplesEventDate>? CouplesEventsUserInivted { get; set; } = new List<CouplesEventDate>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TypeEventUser>? TypeEventsUsers { get; set; } = new List<TypeEventUser>();

        /// <summary>
        /// 
        /// </summary>{ get; set; }
        public ICollection<Event>? EventsCreated = new List<Event>();


    }
}