using AutoMapper;
using GospodaWiki.Dto.Series;
using GospodaWiki.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : Controller
    {
        private readonly ISeriesInterface _seriesRepository;
        private readonly IMapper _mapper;
        public SeriesController(ISeriesInterface seriesRepository, IMapper mapper)
        {
            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SeriesDto>))]
        public IActionResult GetSeries()
        {
            var series = _mapper.Map<List<SeriesDto>>(_seriesRepository.GetSeries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(series);
        }

    }
}
