using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.Models
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AnimalId { get; set; }

        public Animal Animal { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        public int DiagnosisId { get; set; }

        public Diagnosis Diagnosis { get; set; } = null!;

        [Required]
        public DateOnly VisitDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Symptoms { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Notes { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        public ICollection<ProcedureRecord> ProcedureRecords { get; set; } = new List<ProcedureRecord>();

        public bool IsDeleted { get; set; } = false;

    }
}
