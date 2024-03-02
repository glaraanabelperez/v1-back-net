namespace Models
{
    public class CycleEvent
    {
        public int CyclEventId { get; set; }
        public int EventId { get; set; }
        public int CycleId { get; set; }

        public Cycle Cycle { get; set; }
        public Event Event { get; set; }

    }
}