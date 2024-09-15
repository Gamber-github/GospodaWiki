namespace GospodaWiki.Dto.RpgSystem
{
    public class PutRpgSystemDto
    {
        public int RpgSystemId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<int>? StoryIDs { get; set; }
        public ICollection<int>? TagsIds { get; set; }
        public ICollection<int>? CharactersIds { get; set; }
        public ICollection<int>? SeriesIds { get; set; }
    }
}
