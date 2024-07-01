namespace GospodaWiki.Dto.Items
{
    public class GetItemDetailsDto
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<string>? Characters { get; set; }
        public string? OwnerName { get; set; }
        public ICollection<string>? Tags { get; set; }
        public bool isPublished { get; set; } = false;
    }
}