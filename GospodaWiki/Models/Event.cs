using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public Location? Location { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }
}
