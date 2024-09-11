using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Items;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Repository
{
    public class ItemRepository : IItemInterface
    {
        private readonly DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateItem(PostItemDto itemCreate)
        {
            if (itemCreate == null)
            {
                throw new ArgumentNullException(nameof(itemCreate));
            }

            var item = new Item
            {
                Name = itemCreate.Name,
                Description = itemCreate.Description,
                ImagePath = itemCreate.ImagePath,
            };

            _context.Items.Add(item);
            return Save();
        }

        public bool DeleteItem(int itemId)
        {
            var item = _context.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            return Save();      
        }

        public GetItemDetailsDto GetItem(int itemId)
        {
            var itemsContext = _context.Items
                        .Where(i => i.isPublished)
                        .Include(i => i.Characters)
                        .Include(i => i.Tags)
                        .FirstOrDefault(i => i.ItemId == itemId);

            if (itemsContext == null)
            {
                return null;
            }

            var tags = _context.Tags.Where(t => t.isPublished && t.Items.Any(i => i.ItemId == itemId));
            var characters = _context.Characters.Where(c => c.isPublished && c.Items.Any(c => c.Characters == itemsContext.Characters));

            var itemDto = new GetItemDetailsDto
            {
                ItemId = itemsContext.ItemId,
                Name = itemsContext.Name,
                Description = itemsContext.Description,
                ImagePath = itemsContext.ImagePath,
                Characters = characters.Select(c => new GetCharacterReferenceDto 
                {
                    CharacterId = c.CharacterId,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }).ToList(),
                OwnerName = itemsContext.OwnerName,
                Tags = tags.Select(t => new TagReferenceDTO
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                isPublished = itemsContext.isPublished
            };

            return itemDto;
        }
        public ICollection<GetItemsDto> GetItems()
        {
            var items = _context.Items
                .Where(i => i.isPublished)
                .ToList();

            var itemsDto = items.Select(item => new GetItemsDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                isPublished = item.isPublished
            }).ToList();

            return itemsDto;
        }
        public GetItemDetailsDto GetUnpublishedItem(int itemId)
        {
            var items = _context.Items
                .Include(i => i.Characters)
                .Include(i => i.Tags)
                .FirstOrDefault(i => i.ItemId == itemId);

            if (items == null)
            {
                throw new ArgumentException(nameof(items));
            }

            var tags = _context.Tags.Where(t => t.Items.Any(i => i.ItemId == itemId));
            var characters = _context.Characters.Where(c => c.Items.Any(i => i.ItemId == itemId));

            var itemDto = new GetItemDetailsDto
            {
                ItemId = items.ItemId,
                Name = items.Name,
                Description = items.Description,
                ImagePath = items.ImagePath,
                Characters = characters.Select(c => new GetCharacterReferenceDto
                {
                    CharacterId = c.CharacterId,
                    FirstName = c.FirstName,
                    LastName = c.LastName

                }).ToList(),
                OwnerName = items.OwnerName,
                Tags = tags.Select(t => new TagReferenceDTO
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                isPublished = items.isPublished
            };

            return itemDto;     
        }
        public ICollection<GetItemsDto> GetUnpublishedItems()
        {
            var items = _context.Items
                         .ToList();

            var itemsDto = items.Select(item => new GetItemsDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                isPublished = item.isPublished
            }).ToList();

            return itemsDto;
        }
        public bool ItemExists(int itemId)
        {
           return _context.Items.Any(i => i.ItemId == itemId);
        }
        public bool PublishItem(int itemId)
        {
            var item = _context.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (item == null)
            {
                return false;
            }

            item.isPublished = !item.isPublished;
            _context.Items.Update(item);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved >= 0;
        }
        public bool UpdateItem(PutItemDto item, int itemId)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var itemContext = _context.Items
                .Include(i => i.Tags)
                .Include(i => i.Characters)

                .FirstOrDefault(i => i.ItemId == itemId);

            if (itemContext == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            itemContext.Tags.Clear();

            var tags = _context.Tags.Where(t => item.TagIds.Contains(t.TagId)).ToList();
            foreach (var tag in tags)
            {
                itemContext.Tags.Add(tag);
            }

            itemContext.ItemId = itemId;
            itemContext.Name = item.Name;
            itemContext.Description = item.Description;
            itemContext.ImagePath = item.ImagePath;

            _context.Items.Update(itemContext);
            return Save();
        }
    }
}
