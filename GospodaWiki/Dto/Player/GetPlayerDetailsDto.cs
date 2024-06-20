namespace GospodaWiki.Dto.Player
{
    public class GetPlayerDetailsDto
    {
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
        public ICollection<string> Series { get; set; }
        public bool isPublished { get; set; }
    }
}
