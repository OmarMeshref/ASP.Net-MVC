using CompanyManagement.Data;
using CompanyManagement.Models;
using CompanyManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public EmployeesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }


        public async Task<IActionResult> Index(string? search)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.Name.Contains(search));
            }

            var employees = await query.ToListAsync();

            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments =
                _context.Departments.ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments =
                    _context.Departments.ToList();

                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager
                .CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        error.Description);
                }

                ViewBag.Departments =
                    _context.Departments.ToList();

                return View(model);
            }

            await _userManager
                .AddToRoleAsync(user, "Employee");

            string? photoPath = null;

            if (model.Photo != null)
            {
                var folder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "employees");

                Directory.CreateDirectory(folder);

                var fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(model.Photo.FileName);

                var fullPath =
                    Path.Combine(folder, fileName);

                using var stream =
                    new FileStream(
                        fullPath,
                        FileMode.Create);

                await model.Photo.CopyToAsync(stream);

                photoPath =
                    "/uploads/employees/" + fileName;
            }

            var employee = new Employee
            {
                Name = model.Name,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                NationalId = model.NationalId,
                Nationality = model.Nationality,
                MaritalStatus = model.MaritalStatus,
                EntryDate = model.EntryDate,
                DepartmentId = model.DepartmentId,
                PhotoPath = photoPath,
                UserId = user.Id
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Employee created successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}