using System.Collections.Generic;
using System.Threading.Tasks;
using Tasevski.Services.ProductAPI.Models;

namespace Tasevski.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();

        Task<ProductDTO> GetProductById(int productId);

        Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO);

        Task<bool> DeleteProduct(int productId);
    }
}