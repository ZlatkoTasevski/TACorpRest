using System.ComponentModel.DataAnnotations;

namespace Tasevski.Web.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}