using AutoMapper;
using GospodaWiki.Dto.Series;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using GospodaWiki.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{

    [Route("v1/[controller]")]
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSeriesDto>))]
        public IActionResult GetUnpublishedSeries()
        {
            var series = _mapper.Map<List<GetSeriesDto>>(_seriesRepository.GetUnpublishedSeries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(series);
        }

        [HttpGet("{seriesId}")]
        [ProducesResponseType(200, Type = typeof(GetSeriesDetailsDto))]
        public IActionResult GetUnpublishedSeriesById(int seriesId)
        {
            if(!_seriesRepository.SeriesExists(seriesId))
            {
                return NotFound();
            }

            var series = _mapper.Map<GetSeriesDetailsDto>(_seriesRepository.GetUnpublishedSeriesById(seriesId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(series);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSeries([FromBody] PostSeriesDto seriesCreate)
        {
            if (seriesCreate == null)
            {
                return BadRequest(ModelState);
            }

            var series = _seriesRepository.GetSeries().FirstOrDefault(s => s.Name.Trim().ToUpper() == seriesCreate.Name.Trim().ToUpper());

            if (series != null)
            {
                ModelState.AddModelError("", $"Series {series.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seriesMap = _mapper.Map<PostSeriesDto>(seriesCreate);

            if (!_seriesRepository.CreateSeries(seriesMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving series {seriesMap.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpPut("{seriesId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateSeries([FromBody] PutSeriesDto seriesUpdate, [FromRoute] int seriesId)
        {
            if (seriesUpdate == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_seriesRepository.SeriesExists(seriesId))
            {
                ModelState.AddModelError("", $"Series with name {seriesUpdate.Name} does not exist");
                return StatusCode(404, ModelState);
            }

            var seriesMap = _mapper.Map<PutSeriesDto>(seriesUpdate);

            if (!_seriesRepository.UpdateSeries(seriesMap, seriesId))
            {
                ModelState.AddModelError("", $"Something went wrong updating series {seriesUpdate.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Updated");
        }

        [HttpPatch("{seriesId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PublishSeries([FromRoute] int seriesId)
        {
            if (!_seriesRepository.SeriesExists(seriesId))
            {
                ModelState.AddModelError("", $"Series with id {seriesId} does not exist");
                return StatusCode(404, ModelState);
            }

            if (!_seriesRepository.PublishSeries(seriesId))
            {
                ModelState.AddModelError("", $"Something went wrong publishing series with id {seriesId}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Published");
        }
    }
}
