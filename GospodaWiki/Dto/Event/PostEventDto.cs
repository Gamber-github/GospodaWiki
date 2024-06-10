using GospodaWiki.Models;

namespace GospodaWiki.Dto.Event
{
    public class PostEventDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? EventUrl { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? Date { get; set; }
        public int? LocationId { get; set; }
        public ICollection<int>? TagIds { get; set; }
    }
}
