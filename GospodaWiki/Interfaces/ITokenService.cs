using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
