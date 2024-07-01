using GospodaWiki.Data;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class TagRepository : ITagInterface
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTag(PutTagDto tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var tagEntity = new Tag
            {
                Name = tag.Name
            };

            _context.Tags.Add(tagEntity);
            return Save();
            
        }
        public ICollection<GetTagDetailsDto> GetTags()
        {
            var tags = _context.Tags
                .Where(t => t.isPublished)
                .ToList();

            var TagDtos = tags.Select(tags => new GetTagDetailsDto
            {
                TagId = tags.TagId,
                Name = tags.Name,
                isPublished = tags.isPublished
            }).ToList();

            return TagDtos;
        }

        public ICollection<GetTagDetailsDto> GetUnpublishedTags()
        {
            var tags = _context.Tags
                .ToList();

            var TagDtos = tags.Select(tags => new GetTagDetailsDto
            {
                TagId = tags.TagId,
                Name = tags.Name,
                isPublished = tags.isPublished
            }).ToList();

            return TagDtos;
        }

        public bool PublishTag(int tagId)
        {
            var tag = _context.Tags
                .FirstOrDefault(t => t.TagId == tagId);
           
            if (tag == null)
            {
                return false;
            }

            tag.isPublished = true;
            _context.Tags.Update(tag);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        public bool TagExists(int tagId)
        {
            var exists = _context.Tags.Any(t => t.TagId == tagId);
            return exists;
        }

        public bool UpdateTag(int tagId, PutTagDto tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var tagContext = _context.Tags
                .FirstOrDefault(t => t.TagId == tagId);

            if (tagContext == null) {
                throw new ArgumentNullException(nameof(tag));
            }

            tagContext.TagId = tagId;
            tagContext.Name = tag.Name;

            _context.Tags.Update(tagContext);
            return Save();
        }
    }
}
