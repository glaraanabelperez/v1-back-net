namespace Models
{
    public class ProfileDancer
    {
        public int ProfileDanceId { get; set; }
        public int DanceLevelId { get; set; }
        public int DanceRolId { get; set; }
        public int UserId { get; set; }
        public double? Height { get; set; }
        public DanceRol? DanceRol { get; set; } 
        public DanceLevel? DanceLevel { get; set; }

        public User Users { get; set; }

    }
}