using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            ApplicationUser user = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            IEnumerable<ApplicationUser> userList = await _db.Users.ToListAsync();
            return userList;
        }

        //public async Task<object> LockUnlock(string userId)
        //{
        //    var userFromDb = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        //    if(userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
        //    {
        //        userFromDb.LockoutEnd = DateTime.Now;
        //    }
        //    else
        //    {
        //        userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
        //    }            
        //    _db.Users.Update(userFromDb);
        //    await _db.SaveChangesAsync();
        //    return true;
        //}

        public async Task<ApplicationUser> LockUnlock(ApplicationUser user)
        {
            var usersInDb = await _db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (user.LockoutEnd != null && user.LockoutEnd <= DateTime.Now)
             {
                usersInDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                //usersInDb.LockoutEnd = DateTime.Now.AddYears(1000);    
                usersInDb.LockoutEnd = user.LockoutEnd;
            }
            _db.Users.Update(usersInDb);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
