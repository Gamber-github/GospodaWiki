using GospodaWiki.Dto.Player;
using GospodaWiki.Dto.RpgSystem;

namespace GospodaWiki.Dto.Series
{
    public class GetSeriesDto
    {
        public int SeriesId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<string>? Tags { get; set; }
        public ICollection<GetPlayerDetailsDto>? Players { get; set; }
        public ICollection<GospodaWiki.Dto.Character.CharacterDetailsDto>? Characters { get; set; }
        public GetRpgSystemsDto? RpgSystem { get; set; }
        public string? YoutubePlaylistId { get; set; }
        public bool isPublished { get; set; }
    }
}
