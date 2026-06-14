using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.Models
{
    public class ProcedureRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MedicalRecordId { get; set; }

        public MedicalRecord MedicalRecord { get; set; } = null!;

        [Required]
        public int ProcedureId { get; set; }

        public Procedure Procedure { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Notes { get; set; } = string.Empty;

        [Required]
        public int DurationInMinutes { get; set; }

        [MaxLength(100)]
        public string? AnesthesiaUsed { get; set; } 

        public bool IsDeleted { get; set; } = false;
    }
}
