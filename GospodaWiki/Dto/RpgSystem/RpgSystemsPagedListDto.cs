using GospodaWiki.Dto.Player;

namespace GospodaWiki.Dto.RpgSystem
{
    public class RpgSystemsPagedListDto
    {
        public IEnumerable<GetRpgSystemsDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
