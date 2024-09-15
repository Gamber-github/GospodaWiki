using GospodaWiki.Dto.Location;
using GospodaWiki.Dto.Tag;

namespace GospodaWiki.Dto.Event
{
    public class EventDetailsDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string EventUrl { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? Date { get; set; }
        public bool isPublished { get; set; }
        public GetLocationReferenceDTO? Location { get; set; }
        public ICollection<TagReferenceDTO>? Tags { get; set; }
    }
}