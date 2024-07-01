﻿using AutoMapper;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagInterface _tagRepository;
        private readonly IMapper _mapper;
        public TagController(ITagInterface tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetTagDetailsDto>))]
        public IActionResult GetUnpublishedTags(int pageNumber = 1, int pageSize = 10)
        {
            var tags = _tagRepository.GetUnpublishedTags();
            var pagedTags = tags.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedTags = _mapper.Map<List<GetTagDetailsDto>>(pagedTags);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappedTags);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTag([FromBody] PutTagDto tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tagRepository.CreateTag(tag))
            {
                ModelState.AddModelError("", $"Something went wrong saving the tag {tag.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{tagId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateTag(int tagId, [FromBody] PutTagDto tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tagRepository.UpdateTag(tagId, tag))
            {
                ModelState.AddModelError("", $"Something went wrong updating the tag with id {tagId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("{tagId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PublishTag(int tagId)
        {
            if (!_tagRepository.PublishTag(tagId))
            {
                ModelState.AddModelError("", $"Something went wrong publishing the tag with id {tagId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
