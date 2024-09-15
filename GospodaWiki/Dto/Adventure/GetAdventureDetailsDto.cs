using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Tag;

namespace GospodaWiki.Dto.Adventure
{
    public class GetAdventureDetailsDto
    {
        public int AdventureId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public GetSeriesReferenceDto? Series { get; set; }
        public GetRpgSystemReferenceDto? RpgSystem { get; set; }
        public ICollection <GetTagDetailsDto>? Tags { get; set; }
        public ICollection<GetCharacterReferenceDto>? Characters { get; set; }
        public bool isPublished { get; set; }
    }
}
