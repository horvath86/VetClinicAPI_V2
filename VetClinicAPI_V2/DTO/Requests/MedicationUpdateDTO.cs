using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class MedicationUpdateDTO
    {
        [Required(ErrorMessage = "Medication id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Medication code is required.")]
        [MaxLength(10, ErrorMessage = "Medication code cannot exceed 10 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Medication name is required.")]
        [MaxLength(150, ErrorMessage = "Medication name cannot exceed 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Medication description is required.")]
        [MaxLength(250, ErrorMessage = "Medication description cannot exceed 250 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
