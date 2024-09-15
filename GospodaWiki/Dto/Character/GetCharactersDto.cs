using GospodaWiki.Dto.Series;

namespace GospodaWiki.Dto.Character
{
    public class GetCharactersDto
    {
        public int CharacterId { get; set; }
        public string? FullName { get; set;}
        public GetSeriesReferenceDto? SeriesName { get; set; }
        public string? RpgSystemName { get; set; }
        public bool isPublished { get; set; }
    }
}
