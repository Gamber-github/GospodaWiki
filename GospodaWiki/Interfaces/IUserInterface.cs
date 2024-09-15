using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IUserInterface
    {
        AppUser GetUser(string Email);
        bool UserExists(string Email);
    }
}
