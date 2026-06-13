using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(254)]
        public string Email { get; set; } = string.Empty;

        [MinLength(4)]
        [MaxLength(4)]
        public string? LicenceNumber { get; set; }

        [Required]
        [MaxLength(256)]
        public string PassHash { get; set; } = string.Empty;

        [Required]
        public RoleEnum Role { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
