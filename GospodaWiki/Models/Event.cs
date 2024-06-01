using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public Location Location { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = [];
    }
}
