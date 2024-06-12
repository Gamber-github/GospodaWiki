
using GospodaWiki.Dto.Tag;

namespace GospodaWiki.Dto.RpgSystem
{
    public class RpgSystemDetailsDto
    {
        public int RpgSystemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public string? StoryName { get; set; }
        public ICollection<TagDetailsDto>? Tags { get; set; }
        public ICollection<string>? Characters { get; set; }
        public ICollection<string>? Series { get; set; }
    }
}
