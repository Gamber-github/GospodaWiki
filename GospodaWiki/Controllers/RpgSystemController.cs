﻿using AutoMapper;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetRpgSystemsDto>))]
        public IActionResult GetUnpublishedRpgSystems(int pageNumber = 1, int pageSize = 10)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rpgSystems = _mapper.Map<List<GetRpgSystemsDto>>(_rpgSystemRepository.GetUnpublishedRpgSystems());
            var pagedRpgSystems = rpgSystems.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedRpgSystems = _mapper.Map<List<GetRpgSystemsDto>>(pagedRpgSystems);

            var response = new RpgSystemsPagedListDto
            {
                Items = (IEnumerable<GetRpgSystemsDto>)mappedRpgSystems,
                totalItemCount = rpgSystems.Count,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            return Ok(response);
        }

        [HttpGet("{rpgSystemId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetRpgSystemDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUnpublishedRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _mapper.Map<GetRpgSystemDetailsDto>(_rpgSystemRepository.GetUnpublishedRpgSystem(rpgSystemId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rpgSystem);
        }

        [HttpPost]
        [Authorize]
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

        [HttpPut("{rpgSystemId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRpgSystem(int rpgSystemId, [FromBody] PutRpgSystemDto rpgSystemUpdate)
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

            var rpgSystem = _mapper.Map<PutRpgSystemDto>(rpgSystemUpdate);

            if (!_rpgSystemRepository.UpdateRpgSystem(rpgSystem, rpgSystemId))
            {
                ModelState.AddModelError("", $"Something went wrong updating the {rpgSystemUpdate.Name} system.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated.");
        }

        [HttpPatch("{rpgSystemId}/publish")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult PublishRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                ModelState.AddModelError("", $"Something went wrong.");
                return StatusCode(500, ModelState);
            }

            if (!_rpgSystemRepository.PublishRpgSystem(rpgSystemId))
            {
                ModelState.AddModelError("", $"Something went wrong publishing the {rpgSystemId} system.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully published.");
        }

        [HttpDelete("{rpgSystemId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]

        public IActionResult DeleteRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            if (!_rpgSystemRepository.DeleteRpgSystem(rpgSystemId))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the {rpgSystemId} system.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully deleted.");
        }
    }
}
