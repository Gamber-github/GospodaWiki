﻿using AutoMapper;
using GospodaWiki.Dto.Player;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerInterface _playerRepository;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerInterface playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetPlayersDto>))]
        public IActionResult GetUnpublishedPlayers(int pageNumber = 1, int pageSize = 10)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var players = _mapper.Map<List<GetPlayersDto>>(_playerRepository.GetUnpublishedPlayers());
            var pagedPlayers = players.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedPlayers = _mapper.Map<List<GetPlayersDto>>(pagedPlayers);

            var response = new PlayerDTOPagedListDTO
            {
                Items = (IEnumerable<GetPlayersDto>)mappedPlayers,
                totalItemCount = players.Count,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            return Ok(response);
        }

        [HttpGet("{playerId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetPlayerDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUnpublishedPlayer([FromRoute] int playerId)
        {
            if (!_playerRepository.PlayerExists(playerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var player = _mapper.Map<GetPlayerDetailsDto>(_playerRepository.GetUnpublishedPlayer(playerId));

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePlayer ([FromBody] PostPlayerDto playerCreate)
        {
            if (playerCreate == null) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = _playerRepository.GetPlayers().FirstOrDefault(p => p.Fullname.Trim().ToUpper() == playerCreate.FullName.Trim().ToUpper());
            
            if (player != null)
            {
                ModelState.AddModelError("", $"Player {playerCreate.FirstName} {playerCreate.LastName} already exists");
                return StatusCode(422, ModelState);
            }

            var playerMap = _mapper.Map<PostPlayerDto>(playerCreate);

            if(!_playerRepository.CreatePlayer(playerMap))
            {
                ModelState.AddModelError("","Could not save player");
                return BadRequest(ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpPut("{playerId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdatePlayer([FromRoute] int playerId, [FromBody] PutPlayerDto postPlayerDto)
        {
            if (postPlayerDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_playerRepository.PlayerExists(playerId))
            {
                return NotFound();
            }

            var playerMap = _mapper.Map<PutPlayerDto>(postPlayerDto);

            if (!_playerRepository.UpdatePlayer(playerMap, playerId))
            {
                ModelState.AddModelError("", "Could not update player");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Updated");
        }

        [HttpPatch("{playerId}/publish")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult PublishPlayer([FromRoute] int playerId)
        {
            if (!_playerRepository.PlayerExists(playerId))
            {
                return NotFound();
            }

            if (!_playerRepository.PublishPlayer(playerId))
            {
                ModelState.AddModelError("", "Could not publish player");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Published");
        }

        [HttpDelete("{playerId}")]
        [Authorize]
        [ProducesResponseType(204)]

        public IActionResult DeletePlayer([FromRoute] int playerId)
        {
            if (!_playerRepository.PlayerExists(playerId))
            {
                return NotFound();
            }

            if (!_playerRepository.DeletePlayer(playerId))
            {
                StatusCode(500, "Nie można usunąć gracza");
                return StatusCode(500, ModelState);
            }

            return Ok("Gracz usunięty");
        }

    }
}
