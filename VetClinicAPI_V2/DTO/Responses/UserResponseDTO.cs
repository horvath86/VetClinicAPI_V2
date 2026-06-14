using System.ComponentModel.DataAnnotations;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.DTO.Responses
{
    public class UserResponseDTO
    {
        
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? LicenceNumber { get; set; }

        public RoleEnum Role { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
