using Models;

namespace ServicesQueries.Dto
{
    public class ProfileDancerDto
    {
        public int ProfileDanceId { get; set; }
        public int DanceLevelId { get; set; }
        public string DanceLevelName { get; set; }

        public int DanceRolId { get; set; }
        public string DanceRolName{ get; set; }
        public double? Height { get; set; }
        public int? Experience { get; set; }
        public int DanceId { get; set; }
        public string DanceName { get; set; }


    }
}