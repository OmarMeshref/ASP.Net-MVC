using System.ComponentModel.DataAnnotations;

namespace CategoriesProducts.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}