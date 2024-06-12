using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Models;

namespace GospodaWiki.Dto.Series
{
    public class SeriesDto
    {
        public int SeriesId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<string>? Tags { get; set; }
        public ICollection<PlayerDto>? Players { get; set; }
        public ICollection<GospodaWiki.Dto.Character.CharacterDetailsDto>? Characters { get; set; }
        public RpgSystemsDto? RpgSystem { get; set; }
        public string? YoutubePlaylistId { get; set; }
    }
}
