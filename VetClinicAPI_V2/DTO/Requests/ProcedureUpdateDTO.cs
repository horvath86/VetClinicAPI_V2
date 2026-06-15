using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class ProcedureUpdateDTO
    {
        [Required(ErrorMessage = "Procedure id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Procedure code is required.")]
        [MaxLength(10, ErrorMessage = "Procedure code cannot exceed 10 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Procedure name is required.")]
        [MaxLength(200, ErrorMessage = "Procedure name cannot exceed 200 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
