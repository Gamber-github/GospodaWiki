using GospodaWiki.Dto.Series;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ISeriesInterface
    {
        ICollection<SeriesDto> GetSeries();
        SeriesDetailsDto GetSeriesById(int seriesId);
        bool SeriesExists(int seriesId);
        bool CreateSeries(PostSeriesDto series);
        bool Save();
        Task<bool> UpdateSeries(PatchSeriesDto series, int seriesId);
        Task<bool> SaveAsync();

    }
}
