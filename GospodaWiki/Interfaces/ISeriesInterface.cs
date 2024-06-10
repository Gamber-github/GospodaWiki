using GospodaWiki.Dto.Series;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ISeriesInterface
    {
        public ICollection<SeriesDto> GetSeries();
        public SeriesDetailsDto GetSeriesById(int seriesId);
        public bool SeriesExists(int seriesId);
        public bool CreateSeries(PostSeriesDto series);
        public bool Save();
        public bool UpdateSeries(PatchSeriesDto series, int seriesId);

    }
}
