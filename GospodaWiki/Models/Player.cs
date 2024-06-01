namespace GospodaWiki.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;    
        public int Age { get; set; }
        public string About { get; set; } = null!;
        public string Image { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = [];
        public ICollection<PlayerSeries> PlayerSeries { get; set; } = [];
    }
}
