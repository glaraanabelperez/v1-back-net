namespace Models
{
    public class CycleEventDto
    {
        public int CyclEventId { get; set; }
        public int EventId { get; set; }
        public int CycleId { get; set; }

        public CycleDto Cycle { get; set; }

    }
}