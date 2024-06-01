namespace GospodaWiki.Models
{
    public class PlayerSeries
    {
        public int PlayerId{get; set;}
        public int SeriesId { get; set; }
        public Player Player { get; set; } = null!;
        public Series Series { get; set; } = null!;
    }
}
