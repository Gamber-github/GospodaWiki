namespace GospodaWiki.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public Image? Image { get; set; }
        public ICollection<Character>? Characters { get; set; }
        public string? OwnerName { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }
}
