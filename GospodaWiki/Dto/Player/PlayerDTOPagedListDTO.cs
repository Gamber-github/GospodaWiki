namespace GospodaWiki.Dto.Player
{
    public class PlayerDTOPagedListDTO
    {
        public IEnumerable<GetPlayersDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
