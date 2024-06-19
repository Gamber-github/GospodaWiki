namespace GospodaWiki.Models
{
    public class Series
    {
        public int SeriesId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Player>? Players { get; set; }
        public ICollection<Character>? Characters { get; set; }
        public int? RpgSystemId { get; set; }
        public RpgSystem? RpgSystem { get; set; }
        public string? YoutubePlaylistId { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
