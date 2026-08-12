using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityRolesTask.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;
    }
}