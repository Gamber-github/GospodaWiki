using GospodaWiki.Models;

namespace GospodaWiki.Dto.Series
{
    public class PatchSeriesDto
    {
        public int SeriesId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<PlayerDto>? Players { get; set; }
        public ICollection<GospodaWiki.Dto.Character.CharacterDetailsDto>? Characters { get; set; }
        public int? RpgSystemId { get; set; }
        public string? YoutubePlaylistId { get; set; }
    }
}
