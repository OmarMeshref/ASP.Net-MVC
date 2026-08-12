using System.ComponentModel.DataAnnotations;

namespace CategoriesProducts.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? ImagePath { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}