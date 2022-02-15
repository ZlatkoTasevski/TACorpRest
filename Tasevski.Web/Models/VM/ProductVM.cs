using System.Collections.Generic;

namespace Tasevski.Web.Models.VM
{
    public class ProductVM
    {
        public ProductDTO Product { get; set; }
        public IEnumerable<CategoryDTO> CategoriesDTO { get; set; }
    }
}
