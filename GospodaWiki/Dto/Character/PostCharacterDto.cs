namespace GospodaWiki.Dto.Character
{
    public class PostCharacterDto
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? ImagePath { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public int? SeriesId { get; set; }
        public int? RpgSystemId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
