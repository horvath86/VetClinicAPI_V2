using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class DiagnosisUpdateDTO
    {
        [Required(ErrorMessage = "Diagnosis id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Diagnosis code is required.")]
        [MaxLength(10, ErrorMessage = "Diagnosis code cannot exceed 10 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Diagnosis name is required.")]
        [MaxLength(200, ErrorMessage = "Diagnosis name cannot exceed 200 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
