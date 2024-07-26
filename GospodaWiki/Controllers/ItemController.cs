using AutoMapper;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Items;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemInterface _itemInterface;
        private readonly IMapper _mapper;
        public ItemController(IItemInterface itemInterface, IMapper mapper)
        {
            _itemInterface = itemInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetItemsDto>))]
        public IActionResult GetUnpublishedItems(int pageNumber = 1, int pageSize = 10)
        {
            var items = _mapper.Map<List<GetItemsDto>>(_itemInterface.GetUnpublishedItems());
            var pagedCharacters = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedCharacters = _mapper.Map<List<GetItemsDto>>(pagedCharacters);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappedCharacters);
        }

        [HttpGet("{itemId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetItemDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUnpublishedItem(int itemId)
        {
            if (!_itemInterface.ItemExists(itemId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _mapper.Map<GetItemDetailsDto>(_itemInterface.GetUnpublishedItem(itemId));

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(204, Type = typeof(PostItemDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateItem([FromBody] PostItemDto itemCreate)
        {
            if (itemCreate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _mapper.Map<PostItemDto>(itemCreate);

            if (!_itemInterface.CreateItem(itemCreate))
            {
                ModelState.AddModelError("Item", "Item was not created");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut("{itemId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateItem([FromRoute] int itemId, [FromBody] PutItemDto itemUpdate)
        {
            if (itemUpdate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_itemInterface.ItemExists(itemId))
            {
                return NotFound();
            }

            if (!_itemInterface.UpdateItem(itemUpdate, itemId))
            {
                ModelState.AddModelError("Item", "Item was not updated");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPatch("{itemId}/publish")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PublishItem([FromRoute] int itemId)
        {
            if (!_itemInterface.ItemExists(itemId))
            {
                ModelState.AddModelError("Item", "Item does not exist");
                return NotFound(ModelState);
            }

            if (!_itemInterface.PublishItem(itemId))
            {
                ModelState.AddModelError("Item", "Item was not published");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
