using System.ComponentModel.DataAnnotations;

namespace Tasevski.Services.ProductAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
