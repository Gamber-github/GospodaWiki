using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IUserInterface
    {
        User GetUser(string Email);
        bool UserExists(string Email);
    }
}
