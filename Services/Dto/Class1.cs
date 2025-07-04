namespace ServicesQueries.Dto
{
    public class EventInscriptionsDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Inscriptions { get; set; }
        public string? DanceLevelEvent { get; set; }
        public string? DanceRolEvent { get; set; }
        public int? Cupo { get; set; }
        public bool Couple { get; set; }

    }

    public class AllInscriptionsDto : EventInscriptionsDto
    {
        public ICollection<UserInscDto>? listUserInscto { get; set; } = new List<UserInscDto>();
        public ICollection<CouplesEventDateDto>? couples { get; set; } = new List<CouplesEventDateDto>();

    }

    public class MyInscriptionsDto : EventInscriptionsDto
    {
        public UserInscDto user { get; set; } = new UserInscDto();
    }
    public class UserInscDto
    {
        public int UserEventInscriptionId { get; set; }
        public int UserId { get; set; }
        public bool Partner { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? AvatarImage { get; set; }
        public ProfileDancerDto ProfileDancer { get; set; } = new ProfileDancerDto();


    }



}