using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class RpgSystemController : Controller
    {
        private readonly IRpgSystemInterface _rpgSystemRepository;
        private readonly IMapper _mapper;
        public RpgSystemController(IRpgSystemInterface rpgSystemRepository, IMapper mapper)
        {
            _rpgSystemRepository = rpgSystemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RpgSystem>))]
        public IActionResult GetRpgSystems()
        {
            var rpgSystems = _mapper.Map<List<RpgSystemDto>>(_rpgSystemRepository.GetRpgSystems());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rpgSystems);
        }

        [HttpGet("{rpgSystemId}")]
        [ProducesResponseType(200, Type = typeof(RpgSystem))]
        [ProducesResponseType(400)]
        public IActionResult GetRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _mapper.Map<RpgSystem>(_rpgSystemRepository.GetRpgSystem(rpgSystemId));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rpgSystem);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateRpgSystem([FromBody] RpgSystemDto rpgSystemCreate)
        {
            if (rpgSystemCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rpgSystem = _rpgSystemRepository.GetRpgSystems()
                .FirstOrDefault(r => r.Name.Trim().ToUpper() == rpgSystemCreate.Name.Trim().ToUpper());

            if (rpgSystem != null)
            {
                ModelState.AddModelError("", $"RpgSystem {rpgSystemCreate.Name} already exists");   
                return StatusCode(422, ModelState);
            }

            var rpgSystemMap = _mapper.Map<RpgSystem>(rpgSystemCreate);

            if (!_rpgSystemRepository.CreateRpgSystem(rpgSystemMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the {rpgSystemCreate.Name} system.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created.");
        }

        [HttpPut("{rpgSystemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRpgSystem(int rpgSystemId, [FromBody] RpgSystemDto rpgSystemUpdate)
        {
            if (rpgSystemUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (rpgSystemId != rpgSystemUpdate.RpgSystemId)
            {
                return BadRequest(ModelState);
            }

            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _mapper.Map<RpgSystem>(rpgSystemUpdate);

            if (!_rpgSystemRepository.UpdateRpgSystem(rpgSystem))
            {
                ModelState.AddModelError("", $"Something went wrong updating the {rpgSystemUpdate.Name} system.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated.");
        }

        [HttpDelete("{rpgSystemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _rpgSystemRepository.GetRpgSystem(rpgSystemId);

            if (!_rpgSystemRepository.DeleteRpgSystem(rpgSystem))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the {rpgSystem.Name} system.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
