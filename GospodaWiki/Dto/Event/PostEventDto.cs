namespace GospodaWiki.Dto.Event
{
    public class PostEventDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? EventUrl { get; set; }
        public string? ImagePath { get; set; } = "default.jpg";
        public DateTime? Date { get; set; }
    }
}
