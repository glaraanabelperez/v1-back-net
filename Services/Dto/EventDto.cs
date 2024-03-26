

namespace ServicesQueries.Dto
{
    public class EventDto 
    {
        public int EventId { get; set; }
        public int UserIdCreator { get; set; }
        public string? UserName { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }      
        public string? Image { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateFinish { get; set; }

        public int EventStateId { get; set; }
        public string EventStateName { get; set; }

        public int TypeEventId { get; set; }
        public string TypeEventName { get; set; } 

        public int CycleId { get; set; }
        public string CycleTitle { get; set; }
        public string CycleDescription { get; set; }


        public int Cupo { get; set; }

        public int RolId { get; set; }
        public string RolName { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; }

        public int AddressId { get; set; }
        public AddressDto? Address { get; set; }

        //public ICollection<CouplesEventDateDto>? CouplesEvents { get; set; } = new List<CouplesEventDateDto>();


    }
}