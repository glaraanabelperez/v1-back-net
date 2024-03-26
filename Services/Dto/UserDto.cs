namespace ServicesQueries.Dto

{
    public class UserDto 
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
        public ICollection<AddressDto>? Address { get; set; } = new List<AddressDto>();
        /// <summary>
        /// 
        /// </summary>
        public ICollection<ImageDto>? Images { get; set; } = new List<ImageDto>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<UserPermissionDto>? UserPermissions { get; set; } = new List<UserPermissionDto>();
        ///// <summary>
        ///// 
        ///// </summary>
        //public ICollection<Permission>? Permissions { get; set; } = new List<Permission>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ProfileDancerDto>? ProfileDancer { get; set; } = new List<ProfileDancerDto>();
        /// <summary>
        /// 
        /// </summary>
        public ICollection<CouplesEventDateDto>? CouplesEventsUserHost { get; set; } = new List<CouplesEventDateDto>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<CouplesEventDateDto>? CouplesEventsUserInivted { get; set; } = new List<CouplesEventDateDto>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TypeEventUserDto>? TypeEventsUsers { get; set; } = new List<TypeEventUserDto>();

        /// <summary>
        /// 
        /// </summary>{ get; set; }
        public ICollection<EventDto>? EventsCreated = new List<EventDto>();


    }
}