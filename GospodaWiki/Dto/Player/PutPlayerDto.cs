namespace GospodaWiki.Dto.Player
{
    public class PutPlayerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
        public ICollection<int>? SeriesId { get; set; }
    }
}
