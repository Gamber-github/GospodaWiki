namespace GospodaWiki.Models
{
    public class PlayerSeries
    {
        public int PlayerId{get; set;}
        public Player Player {get; set;}
        public int SeriesId{get; set;}
        public Series Series {get; set;}
    }
}
