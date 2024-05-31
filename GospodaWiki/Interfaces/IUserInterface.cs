using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IUserInterface
    {
        User GetUser(int id);
        bool UserExists(int userId);
    }
}
