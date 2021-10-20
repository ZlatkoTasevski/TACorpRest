using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.Identity.DbContexts;
using Tasevski.Services.Identity.Models;

namespace Tasevski.Services.Identity.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _db;
        public UserRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            IEnumerable<ApplicationUser> userList = await _db.Users.ToListAsync();
            return userList;
        }
    }
}
