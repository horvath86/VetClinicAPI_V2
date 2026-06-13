using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

        public bool IsDeleted { get; set; } = false;
    }
}
