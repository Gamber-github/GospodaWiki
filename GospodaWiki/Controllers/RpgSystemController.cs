using AutoMapper;
using GospodaWiki.Dto.RpgSystem;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<RpgSystemsDto>))]
        public IActionResult GetRpgSystems()
        {
            var rpgSystems = _mapper.Map<List<RpgSystemsDto>>(_rpgSystemRepository.GetRpgSystems());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rpgSystems);
        }

        [HttpGet("{rpgSystemId}")]
        [ProducesResponseType(200, Type = typeof(RpgSystemDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _mapper.Map<RpgSystemDetailsDto>(_rpgSystemRepository.GetRpgSystem(rpgSystemId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rpgSystem);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRpgSystem([FromBody] PostRpgSystemDto rpgSystemCreate)
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

            var rpgSystemMap = _mapper.Map<PostRpgSystemDto>(rpgSystemCreate);

            if (!_rpgSystemRepository.CreateRpgSystem(rpgSystemMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the {rpgSystemCreate.Name} system.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created.");
        }

        [HttpPatch("{rpgSystemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRpgSystem(int rpgSystemId, [FromBody] PatchRpgSystemDto rpgSystemUpdate)
        {
            if (rpgSystemUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _mapper.Map<PatchRpgSystemDto>(rpgSystemUpdate);

            if (!await _rpgSystemRepository.UpdateRpgSystem(rpgSystem, rpgSystemId))
            {
                ModelState.AddModelError("", $"Something went wrong updating the {rpgSystemUpdate.Name} system.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
