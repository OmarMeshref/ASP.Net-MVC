using CompanyManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string NationalId { get; set; } = string.Empty;

        [Required]
        public string Nationality { get; set; } = string.Empty;

        public MaritalStatus MaritalStatus { get; set; }

        public DateTime EntryDate { get; set; }

        public int DepartmentId { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
