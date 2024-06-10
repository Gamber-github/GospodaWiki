using GospodaWiki.Models;

namespace GospodaWiki.Dto.RpgSystem
{
    public class PatchRpgSystemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public string? StoryName { get; set; }
        public ICollection<int>? TagsIds { get; set; }
        public ICollection<int>? CharactersIds { get; set; }
        public ICollection<int>? SeriesIds { get; set; }
    }
}
