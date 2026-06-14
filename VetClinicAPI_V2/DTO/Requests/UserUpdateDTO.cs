using System.ComponentModel.DataAnnotations;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "User id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email address format.")]
        [MaxLength(254, ErrorMessage = "Email address cannot exceed 254 characters.")]
        public string Email { get; set; } = string.Empty;

        [MinLength(4, ErrorMessage = "Licence number must be exactly 4 digits.")]
        [MaxLength(4, ErrorMessage = "Licence number must be exactly 4 digits.")]
        public string? LicenceNumber { get; set; }

        [Required(ErrorMessage = "Role must be selected")]
        public RoleEnum Role { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
