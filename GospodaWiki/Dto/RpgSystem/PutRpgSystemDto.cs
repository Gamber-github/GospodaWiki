namespace GospodaWiki.Dto.RpgSystem
{
    public class PutRpgSystemDto
    {
        public int RpgSystemId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public string? StoryName { get; set; }
        public ICollection<int>? TagsId { get; set; }
        public ICollection<int>? CharactersId { get; set; }
        public ICollection<int>? SeriesId { get; set; }
    }
}
