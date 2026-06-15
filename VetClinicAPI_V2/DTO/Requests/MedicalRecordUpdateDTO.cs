using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class MedicalRecordUpdateDTO
    {
        [Required(ErrorMessage = "Medical record id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Animal id is required.")]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Veterinarian id is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Diagnosis selection is required.")]
        public int DiagnosisId { get; set; }

        [Required(ErrorMessage = "Visit date is required.")]
        public DateOnly VisitDate { get; set; }

        [Required(ErrorMessage = "Patient symptoms are required.")]
        [MaxLength(200, ErrorMessage = "Patient symptoms cannot exceed 200 characters.")]
        public string Symptoms { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string? Notes { get; set; }
    }
}
