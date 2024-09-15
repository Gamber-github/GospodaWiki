namespace GospodaWiki.Models
{
    public class Adventure
    {
        public int AdventureId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public RpgSystem? RpgSystem { get; set; }
        public int? RpgSystemId { get; set; }
        public Series? Series { get; set; }
        public int? SeriesId { get; set; }
        public ICollection<Character>? Characters { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
