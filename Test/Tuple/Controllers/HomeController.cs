using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TupleTask.Models;

namespace TupleTask.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult StudentCourse()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Omar",
                Age = 24
            };

            Course course = new Course
            {
                CourseId = 101,
                CourseName = "ASP.NET Core MVC",
                InstructorName = "Ahmad Mohsn"
            };

            var data = Tuple.Create(student, course);
            return View(data);
        }
    }
}
