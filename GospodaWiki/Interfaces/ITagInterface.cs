using GospodaWiki.Dto.Tag;

namespace GospodaWiki.Interfaces
{
    public interface ITagInterface
    {
        ICollection<GetTagDetailsDto> GetTags();
        ICollection<GetTagDetailsDto> GetUnpublishedTags();
        bool TagExists(int tagId);
        bool CreateTag(PutTagDto tag);
        bool UpdateTag(int tagId, PutTagDto tag);
        bool Save();
        bool PublishTag(int tagId);
        bool DeleteTag(int tagId);
    }
}
