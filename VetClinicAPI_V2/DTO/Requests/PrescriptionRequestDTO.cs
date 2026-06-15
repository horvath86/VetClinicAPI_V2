using System.ComponentModel.DataAnnotations;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class PrescriptionRequestDTO
    {
        [Required(ErrorMessage = "Medication selection is required.")]
        public int MedicationId { get; set; }

        [Required(ErrorMessage = "Dosage instructions are required.")]
        [MaxLength(150, ErrorMessage = "Dosage instructions cannot exceed 150 characters.")]
        public string DosageInstructions { get; set; } = string.Empty;
    }
}
