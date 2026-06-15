using System.ComponentModel.DataAnnotations;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class AppointmentUpdateDTO
    {
        [Required(ErrorMessage = "Appointment id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Owner's name is required.")]
        [MaxLength(100, ErrorMessage = "Owner's name cannot exceed 100 characters.")]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner's phone number is required.")]
        [MaxLength(20, ErrorMessage = "Owner's phone number cannot exceed 20 characters.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Animal's name is required.")]
        [MaxLength(50, ErrorMessage = "Animal's name cannot exceed 50 characters.")]
        public string AnimalName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Veterinarian's id is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Scheduled date and time are required.")]
        public DateTime ScheduledDateTime { get; set; }

        public StatusEnum Status { get; set; }
    }
}
