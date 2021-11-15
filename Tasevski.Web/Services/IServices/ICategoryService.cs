using System.Threading.Tasks;
using Tasevski.Web.Models;

namespace Tasevski.Web.Services.IServices
{
    public interface ICategoryService : IBaseService
    {
        Task<T> GetAllCategoriesAsync<T>(string token);

        Task<T> GetCategoryByIdAsync<T>(int id, string token);

        Task<T> CreateCategoryAsync<T>(CategoryDTO categoryDTO, string token);

        Task<T> UpdateCategoryAsync<T>(CategoryDTO categoryDTO, string token);

        Task<T> DeleteCategoryAsync<T>(int id, string token);
    }
}
