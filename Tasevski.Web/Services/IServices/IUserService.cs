using System.Threading.Tasks;

namespace Tasevski.Web.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<T> GetAllUsersAsync<T>(string token);
    }
}
