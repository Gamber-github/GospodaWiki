﻿using AutoMapper;
using GospodaWiki.Dto.Location;
using GospodaWiki.Interfaces;
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
        public IActionResult GetUnpublishedLocations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locations = _locationRepository.GetUnpublishedLocations();

            return Ok(locations);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(LocationDetailsDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUnpublishedLocation(int locationId)
        {
            if (!await _locationRepository.LocationExists(locationId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = _locationRepository.GetUnpublishedLocation(locationId);

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

        [HttpPut("{locationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task <IActionResult> UpdateLocation([FromRoute] int locationId, [FromBody] PutLocationDto locationUpdate)
        {
            if (locationUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _locationRepository.LocationExists(locationId))
            {
                return NotFound();
            }

            var locationMap = _mapper.Map<PutLocationDto>(locationUpdate);

            if (!await _locationRepository.UpdateLocation(locationMap, locationId))
            {
                ModelState.AddModelError("", $"Something went wrong updating {locationUpdate.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        
        [HttpPatch("{locationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PublishLocation([FromRoute] int locationId)
        {
            if (!await _locationRepository.LocationExists(locationId))
            {
                ModelState.AddModelError("", $"Something went wrong.");
                return NotFound(ModelState);
            }

            if (!await _locationRepository.PublishLocation(locationId))
            {
                ModelState.AddModelError("", $"Something went wrong publishing location.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Published");
        }
    }
}
