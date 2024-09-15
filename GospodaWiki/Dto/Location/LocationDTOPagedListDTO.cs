namespace GospodaWiki.Dto.Location
{
    public class LocationDTOPagedListDTO
    {
        public IEnumerable<GetLocationsDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
