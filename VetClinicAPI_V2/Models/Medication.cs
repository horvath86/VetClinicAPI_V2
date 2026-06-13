using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
