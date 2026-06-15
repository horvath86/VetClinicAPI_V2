using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class ProcedureRecordUpdateDTO
    {
        [Required(ErrorMessage = "Procedure record id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Procedure selection is required.")]
        public int ProcedureId { get; set; }

        [Required(ErrorMessage = "Procedure notes are required.")]
        [MaxLength(200, ErrorMessage = "Procedure notes cannot exceed 200 characters.")]
        public string Notes { get; set; } = string.Empty;

        [Required(ErrorMessage = "Duration in minutes is required.")]
        [Range(1, 480, ErrorMessage = "Duration must be between 1 and 480 minutes.")]
        public int DurationInMinutes { get; set; }

        [MaxLength(100, ErrorMessage = "Anesthesia details cannot exceed 100 characters.")]
        public string? AnesthesiaUsed { get; set; }
    }
}
