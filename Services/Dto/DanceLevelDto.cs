namespace ServicesQueries.Dto
{
    public class DanceLevelDto
    {
        public int DanceLevelId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<EventDto>? Events { get; set; } = new List<EventDto>();

        //public ICollection<ProfileDancer>? ProfileDancers { get; set; } = new List<ProfileDancer>();
        //public ICollection<DanceRol>? DanceRols { get; }

    }
}