using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Player;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Tag;

namespace GospodaWiki.Dto.Series
{
    public class GetSeriesDto
    {
        public int SeriesId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<TagReferenceDTO>? Tags { get; set; }
        public ICollection<GetPlayerReferenceDto>? Players { get; set; }
        public ICollection<GetCharacterReferenceDto>? Characters { get; set; }
        public GetRpgSystemReferenceDto? RpgSystem { get; set; }
        public string? YoutubePlaylistId { get; set; }
        public bool isPublished { get; set; }
    }
}
