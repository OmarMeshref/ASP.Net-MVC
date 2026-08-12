using System.ComponentModel.DataAnnotations;

namespace CategoriesProducts.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}