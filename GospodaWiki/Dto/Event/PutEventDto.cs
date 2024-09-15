namespace GospodaWiki.Dto.Event
{
    public class PutEventDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? EventUrl { get; set; }
        public string? ImagePath { get; set; }
        public string? Date { get; set; }
        public int? LocationId { get; set; }
        public ICollection<int>? TagIds { get; set; }
    }
}
