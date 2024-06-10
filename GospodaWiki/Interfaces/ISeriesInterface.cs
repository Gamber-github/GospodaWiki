using GospodaWiki.Dto.Series;

namespace GospodaWiki.Interfaces
{
    public interface ISeriesInterface
    {
        public ICollection<SeriesDto> GetSeries();

    }
}
