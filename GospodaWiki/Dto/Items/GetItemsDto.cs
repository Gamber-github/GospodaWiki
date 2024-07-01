namespace GospodaWiki.Dto.Items
{
    public class GetItemsDto
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public bool isPublished { get; set; } = false;
    }
}