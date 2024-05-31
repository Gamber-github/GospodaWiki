using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("api/[controller]")]
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

        public IActionResult getRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _mapper.Map<List<RpgSystemDto>>(_rpgSystemRepository.GetRpgSystem(rpgSystemId));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rpgSystem);
        }
    }
}
