using GospodaWiki.Dto.Player;

namespace GospodaWiki.Dto.Series
{
    public class SerieDTOPagedListDTO
    {
        public IEnumerable<GetSeriesDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
