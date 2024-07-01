using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Data
{
    public class UserDataContext : IdentityDbContext
    {
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
        {

        }
    }
}
