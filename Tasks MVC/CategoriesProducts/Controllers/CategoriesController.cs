using CategoriesProducts.Data;
using CategoriesProducts.Models;
using CategoriesProducts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CategoriesProducts.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CategoriesController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var categories =
                await _context.Categories.ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "categories");

            Directory.CreateDirectory(folderPath);

            var fileName =
                Guid.NewGuid().ToString()
                + Path.GetExtension(model.Image.FileName);

            var fullPath =
                Path.Combine(folderPath, fileName);

            using (var stream =
                   new FileStream(fullPath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var category = new Category
            {
                Name = model.Name,
                ImagePath =
                    "/uploads/categories/" + fileName
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}