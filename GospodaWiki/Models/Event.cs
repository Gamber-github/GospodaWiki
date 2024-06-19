using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? EventUrl { get; set; }
        public Image? Image { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? Date { get; set; }
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
