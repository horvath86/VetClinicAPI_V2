using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MedicalRecordId { get; set; }

        public MedicalRecord MedicalRecord { get; set; } = null!;

        [Required]
        public int MedicationId { get; set; }

        public Medication Medication { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string DosageInstructions { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
