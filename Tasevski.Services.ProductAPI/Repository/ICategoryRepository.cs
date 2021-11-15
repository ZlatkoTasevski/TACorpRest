using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.ProductAPI.Models.Dto;

namespace Tasevski.Services.ProductAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();

        Task<CategoryDTO> GetCategoryById(int categoryId);

        Task<CategoryDTO> CreateUpdateCategory(CategoryDTO categoryDTO);

        Task<bool> DeleteCategory(int categoryId);
    }
}
