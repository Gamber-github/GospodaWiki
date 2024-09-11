
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Player;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Story;
using GospodaWiki.Dto.Tag;

namespace GospodaWiki.Dto.RpgSystem
{
    public class GetRpgSystemDetailsDto
    {
        public int RpgSystemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<GetStoryReferenceDTO>? Stories { get; set; }
        public bool isPublished { get; set; }
        public ICollection<TagReferenceDTO>? Tags { get; set; }
        public ICollection<GetCharacterReferenceDto>? Characters { get; set; }
        public ICollection<GetSeriesReferenceDto>? Series { get; set; }
    }
}
