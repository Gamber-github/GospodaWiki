namespace GospodaWiki.Dto.Player
{
    public class PostPlayerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int? Age { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
    }
}
