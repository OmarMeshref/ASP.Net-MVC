using Microsoft.AspNetCore.Mvc;
using Validation.Models;

namespace Validation.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            TempData["SuccessMessage"] = "Employee created successfully.";
            return RedirectToAction("Success");
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
