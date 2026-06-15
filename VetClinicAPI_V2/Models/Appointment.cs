using System.ComponentModel.DataAnnotations;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string OwnerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string AnimalName { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        public DateTime ScheduledDateTime { get; set; }

        public StatusEnum Status { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
