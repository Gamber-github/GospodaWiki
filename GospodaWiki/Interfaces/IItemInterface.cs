using GospodaWiki.Dto.Items;

namespace GospodaWiki.Interfaces
{
    public interface IItemInterface
    {
        ICollection<GetItemsDto> GetItems();
        ICollection<GetItemsDto> GetUnpublishedItems();
        GetItemDetailsDto GetItem(int id);
        GetItemDetailsDto GetUnpublishedItem(int itemId);
        bool ItemExists(int itemId);
        bool CreateItem(PostItemDto item);
        bool UpdateItem(PutItemDto item, int itemId);
        bool Save();
        bool PublishItem(int itemId);
        bool DeleteItem(int itemId);
    }
}
