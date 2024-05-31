using GospodaWiki.Models;

namespace GospodaWiki.Dto
{
    public class RpgSystemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = [];
    }
}
