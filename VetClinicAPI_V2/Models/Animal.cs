using System.ComponentModel.DataAnnotations;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public SpeciesEnum Species { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public Owner Owner { get; set; } = null!;

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

        public bool IsDeleted { get; set; } = false;

    }
}
