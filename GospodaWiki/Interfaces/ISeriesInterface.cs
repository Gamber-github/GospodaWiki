using GospodaWiki.Dto.Series;

namespace GospodaWiki.Interfaces
{
    public interface ISeriesInterface
    {
        ICollection<GetSeriesDto> GetSeries();
        ICollection<GetSeriesDto> GetUnpublishedSeries();
        GetSeriesDetailsDto GetSeriesById(int seriesId);
        GetSeriesDetailsDto GetUnpublishedSeriesById(int seriesId);
        bool SeriesExists(int seriesId);
        bool CreateSeries(PostSeriesDto series);
        bool Save();
        bool UpdateSeries(PutSeriesDto series, int seriesId);
        bool PublishSeries(int seriesId);
    }
}
