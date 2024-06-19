namespace GospodaWiki.Dto.Event
{
    public class EventsDto
    {
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string EventUrl { get; set; }
        public string ImagePath { get; set; }
        public DateTime? Date { get; set; }
        public bool isPublished { get; set; }
    }
}
