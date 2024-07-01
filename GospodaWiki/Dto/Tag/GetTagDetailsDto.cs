namespace GospodaWiki.Dto.Tag
{
    public class GetTagDetailsDto
    {
        public int TagId { get; set; }
        public string? Name { get; set; }
        public bool isPublished { get; set; }
    }
}
