using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public User GetUser(int id)
        {
            return _context.Users.Where(p => p.UserId == id).FirstOrDefault();
        }
        public bool UserExists(int userId)
        {
            return _context.Users.Any(p => p.UserId == userId);
        }
    }
}
