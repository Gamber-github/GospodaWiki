namespace GospodaWiki.Dto.Player
{
    public class GetPlayersDto
    {
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Fullname => $"{FirstName} {LastName}";
        public bool isPublished { get; set; }
    }
}
