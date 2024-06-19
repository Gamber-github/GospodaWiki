using GospodaWiki.Dto.Tag;

namespace GospodaWiki.Dto.Series
{
    public class GetSeriesDetailsDto
    {
        public int SeriesId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<TagDetailsDto>? Tags { get; set; }
        public ICollection<PlayerDto>? Players { get; set; }
        public ICollection<GospodaWiki.Dto.Character.CharacterDetailsDto>? Characters { get; set; }
        public int? RpgSystemId { get; set; }
        public GospodaWiki.Dto.RpgSystem.GetRpgSystemsDto? RpgSystem { get; set; }
        public string? YoutubePlaylistId { get; set; }
        public bool isPublished { get; set; }
    }
}
