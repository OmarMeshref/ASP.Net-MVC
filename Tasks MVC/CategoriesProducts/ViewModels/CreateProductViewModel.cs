using System.ComponentModel.DataAnnotations;

namespace CategoriesProducts.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}