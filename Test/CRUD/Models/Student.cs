using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Range(18, 70, ErrorMessage = "Age must be between 18-70")]
        public int Age { get; set; }
    }
}
