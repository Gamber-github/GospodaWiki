namespace GospodaWiki.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string? Name { get; set; } = null!;
        public ICollection<Character>? Characters { get; set; }
        public ICollection<RpgSystem>? RpgSystems { get; set; }
        public ICollection<Series>? Series { get; set; }
        public ICollection<Event>? Events { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<Story>? Stories { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
