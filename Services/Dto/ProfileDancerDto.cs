namespace ServicesQueries.Dto
{
    public class ProfileDancerDto
    {
        public int ProfileDanceId { get; set; }
        public int DanceLevelId { get; set; }
        public int DanceRolId { get; set; }
        public int UserId { get; set; }
        public double? Height { get; set; }
        public DanceRolDto? DanceRol { get; set; } 
        public DanceLevelDto? DanceLevel { get; set; }

        public UserDto Users { get; set; }

    }
}