namespace GospodaWiki.Models
{
    public class Image
    { 
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int? CharacterId { get; set; }
        public Character? Character { get; set; }
        public int? EventId { get; set; }
        public Event? Event { get; set; }
        public int? ItemId { get; set; }
        public Item? Item { get; set; }
        public int? RpgSystemId { get; set; }
        public RpgSystem? RpgSystem { get; set; }
    }
}
