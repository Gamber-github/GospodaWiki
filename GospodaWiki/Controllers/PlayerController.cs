using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerInterface playerInterface;
        private readonly IMapper mapper;

        public PlayerController(IPlayerInterface playerInterface, IMapper mapper)
        {
            this.playerInterface = playerInterface;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
        public IActionResult GetPlayers()
        {
            var players = mapper.Map<List<PlayerDto>>(playerInterface.GetPlayers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(players);
        }
    }
}
