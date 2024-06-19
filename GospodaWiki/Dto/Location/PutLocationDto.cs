namespace GospodaWiki.Dto.Location
{
    public class PutLocationDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? LocationURL { get; set; }
        public ICollection<int>? EventId { get; set; }
    }
}
