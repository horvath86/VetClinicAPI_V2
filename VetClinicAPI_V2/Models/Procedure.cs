using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.Models
{
    public class Procedure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
