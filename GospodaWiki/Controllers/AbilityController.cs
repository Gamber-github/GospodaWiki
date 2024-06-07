using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AbilityController : Controller
    {
        private readonly IAbilityInterface _abilityInterface;
        private readonly IMapper _mapper;

        public AbilityController(IAbilityInterface abilityInterface, IMapper mapper)
        {
            _abilityInterface = abilityInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200 , Type = typeof(IEnumerable<Ability>))]
        public IActionResult GetAbilities()
        {
            var abilities = _mapper.Map<List<AbilityDto>>(_abilityInterface.GetAbilities());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(abilities);
        }
        
    }
}
