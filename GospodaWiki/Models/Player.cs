namespace GospodaWiki.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }  
        public int? Age { get; set; }
        public string? About { get; set; }
        public string? Image { get; set; }
        public ICollection<Series>? Series { get; set; } 
    }
}
