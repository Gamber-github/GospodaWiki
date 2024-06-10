using AutoMapper;
using GospodaWiki.Dto.Location;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationInterface _locationRepository;
        private readonly IMapper _mapper;

        public LocationController(ILocationInterface locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(LocationDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetLocations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locations = _locationRepository.GetLocations();

            return Ok(locations);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(LocationDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetLocation(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = _locationRepository.GetLocation(locationId);

            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateLocation([FromBody] PostLocationDto locationCreate)
        {
            if (locationCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = _locationRepository.GetLocations().FirstOrDefault(l => l.Name.Trim().ToUpper() == locationCreate.Name.Trim().ToUpper());

            if (location != null)
            {
                ModelState.AddModelError("", $"Location {locationCreate.Name} already exists");
                return StatusCode(422, ModelState);
            }

            var locationMap = _mapper.Map<PostLocationDto>(locationCreate);

            if (!_locationRepository.CreateLocation(locationMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving {locationCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpPatch("{locationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateLocation([FromRoute] int locationId, [FromBody] PatchLocationDto locationUpdate)
        {
            if (locationUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_locationRepository.LocationExists(locationId))
            {
                return NotFound();
            }

            var locationMap = _mapper.Map<PatchLocationDto>(locationUpdate);

            if (!_locationRepository.UpdateLocation(locationMap, locationId))
            {
                ModelState.AddModelError("", $"Something went wrong updating {locationUpdate.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
