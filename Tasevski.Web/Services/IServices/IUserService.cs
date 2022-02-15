using System.Threading.Tasks;
using Tasevski.Web.Models;

namespace Tasevski.Web.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<T> GetAllUsersAsync<T>(string token);
        Task<T> LockUnlock<T>(ApplicationUser user, string token);
        Task<T> GetUserByIdAsync<T>(string userId, string token);
    }
}
