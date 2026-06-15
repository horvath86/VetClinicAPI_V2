using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class PrescriptionUpdateDTO
    {
        [Required(ErrorMessage = "Prescription id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Medication selection is required.")]
        public int MedicationId { get; set; }

        [Required(ErrorMessage = "Dosage instructions are required.")]
        [MaxLength(150, ErrorMessage = "Dosage instructions cannot exceed 150 characters.")]
        public string DosageInstructions { get; set; } = string.Empty;
    }
}
