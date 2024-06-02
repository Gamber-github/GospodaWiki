namespace GospodaWiki.Models
{
    public class Series
    {
        public int SeriesId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Player>? Players { get; set; }
        public RpgSystem? RpgSystem { get; set; }
    }
}
