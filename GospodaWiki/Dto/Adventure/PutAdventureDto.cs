namespace GospodaWiki.Dto.Adventure
{
    public class PutAdventureDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int RpgSystemId { get; set; }
        public int SeriesId { get; set; }
        public ICollection<int>? TagsIds { get; set; }
        public ICollection<int>? CharactersIds { get; set; }
    }
}