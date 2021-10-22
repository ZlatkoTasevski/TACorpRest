using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.Identity.Models;

namespace Tasevski.Services.Identity.Repository
{
    public interface IUserRepo
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserById(string userId);
        //Task<object> LockUnlock(string userId);
        Task<ApplicationUser> LockUnlock(ApplicationUser user);
    }
}
