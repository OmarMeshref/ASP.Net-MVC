using CategoriesProducts.Data;
using CategoriesProducts.Models;
using CategoriesProducts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CategoriesProducts.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var products =
                await _context.Products
                    .Include(p => p.Category)
                    .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories =
                new SelectList(
                    await _context.Categories.ToListAsync(),
                    "Id",
                    "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories =
                    new SelectList(
                        await _context.Categories.ToListAsync(),
                        "Id",
                        "Name");

                return View(model);
            }

            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "products");

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

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                ImagePath =
                    "/uploads/products/" + fileName
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}