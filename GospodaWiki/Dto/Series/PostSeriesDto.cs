using GospodaWiki.Models;

namespace GospodaWiki.Dto.Series
{
    public class PostSeriesDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<int>? Tags { get; set; }
        public ICollection<int>? Players { get; set; }
        public ICollection<int>? Characters { get; set; }
        public int? RpgSystemId { get; set; }
        public string? YoutubePlaylistId { get; set; }
    }
}
