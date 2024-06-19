namespace GospodaWiki.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageId { get; set; }
        public Image? Image { get; set; } 
        public string? ImagePath { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public int? SeriesId { get; set; }
        public Series? Series { get; set; }
        public int? RpgSystemId { get; set; }
        public RpgSystem? RpgSystem { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Item>? Items { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
