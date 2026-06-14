using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class OwnerCreateDTO
    {
        [Required(ErrorMessage = "Owner's name is required.")]
        [MaxLength(100, ErrorMessage = "Owner's name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner's phone number is required.")]
        [MaxLength(20, ErrorMessage = "Owner's phone number cannot exceed 20 characters.")]
        public string Phone { get; set; } = string.Empty;
    }
}
