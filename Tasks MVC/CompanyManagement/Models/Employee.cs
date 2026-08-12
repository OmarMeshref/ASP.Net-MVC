using CompanyManagement.Data;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string NationalId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Nationality { get; set; } = string.Empty;

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        public string? PhotoPath { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public ICollection<EmployeeTask> Tasks { get; set; } = new List<EmployeeTask>();
    }
}
