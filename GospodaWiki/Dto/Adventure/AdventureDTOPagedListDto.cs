namespace GospodaWiki.Dto.Adventure
{
    internal class AdventureDTOPagedListDto
    {
        public IEnumerable<GetAdventuresDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}