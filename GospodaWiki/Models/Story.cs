namespace GospodaWiki.Models
{
    public class Story
    {
        public int StoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RpgSystemId { get; set; }
        public RpgSystem RpgSystem { get; set; }
        public string? YoutubeVideoUrl { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
