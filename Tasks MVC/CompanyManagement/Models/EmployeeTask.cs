using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.Models
{
    public class EmployeeTask
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public TaskImportance Importance { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}
