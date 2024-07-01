namespace GospodaWiki.Dto.Items
{
    public class PutItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<int>? TagIds { get; set; }
    }
}