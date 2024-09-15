using AutoMapper;
using GospodaWiki.Dto.Adventure;
using GospodaWiki.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AdventureController: Controller
    {
        private readonly IAdventureInterface _adventureRepository;
        private readonly IMapper _mapper;
        public AdventureController(IAdventureInterface adventureRepository, IMapper mapper)
        {
            _adventureRepository = adventureRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetAdventuresDto>))]

        public IActionResult GetUnpublishedAdventures (int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adventures = _mapper.Map<List<GetAdventuresDto>>(_adventureRepository.GetUnpublishedAdventures());
            var pagedAdventures = adventures.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedAdventures = _mapper.Map<List<GetAdventuresDto>>(pagedAdventures);

            var response = new AdventureDTOPagedListDto
            {
                Items = (IEnumerable<GetAdventuresDto>)mappedAdventures,
                totalItemCount = adventures.Count,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            return Ok(response);
        }

        [HttpGet("{adventureId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetAdventureDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUnpublishedAdventure(int adventureId)
        {
            if (!_adventureRepository.AdventureExists(adventureId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adventure = _mapper.Map<GetAdventureDetailsDto>(_adventureRepository.GetUnpublishedAdventure(adventureId));
            return Ok(adventure);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAdventure([FromBody] PostAdventureDto adventuretoCreate)
        {
            if(adventuretoCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adventure = _adventureRepository.GetAdventures().FirstOrDefault(a => a.Title.Trim().ToUpper() == adventuretoCreate.Title.Trim().ToUpper());

            if (adventure != null)
            {
                return BadRequest("Adventure already exists");
            }

            var adventrureMap = _mapper.Map<PostAdventureDto>(adventuretoCreate);

            if (!_adventureRepository.CreateAdventure(adventrureMap))
            {
                return BadRequest("Adventure could not be created");
            }

            return Ok();
        }

        [HttpPut("{adventureId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateAdventure([FromBody] PutAdventureDto adventureToUpdate, int adventureId)
        {
            if (adventureToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_adventureRepository.AdventureExists(adventureId))
            {
                return NotFound();
            }

            var adventure = _mapper.Map<PutAdventureDto>(adventureToUpdate);

            if (!_adventureRepository.UpdateAdventure(adventure, adventureId))
            {
                return BadRequest("Adventure could not be updated");
            }

            return Ok();
        }

        [HttpDelete("{adventureId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteAdventure(int adventureId)
        {
            if (!_adventureRepository.AdventureExists(adventureId))
            {
                return NotFound();
            }

            if (!_adventureRepository.DeleteAdventure(adventureId))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the adventure.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPatch("{adventureId}/publish")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PublishAdventure(int adventureId)
        {
            if (!_adventureRepository.AdventureExists(adventureId))
            {
                return NotFound();
            }

            if (!_adventureRepository.PublishAdventure(adventureId))
            {
                return BadRequest("Adventure could not be published");
            }

            return Ok();
        }
    }
}
