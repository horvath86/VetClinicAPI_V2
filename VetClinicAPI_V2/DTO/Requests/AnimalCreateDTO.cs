using System.ComponentModel.DataAnnotations;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.DTO.Requests
{
    public class AnimalCreateDTO
    {
        [Required(ErrorMessage = "Animal's code is required.")]
        [MaxLength(20, ErrorMessage = "Animal's code cannot exceed 20 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Animal's name is required.")]
        [MaxLength(50, ErrorMessage = "Animal's name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Animal's species is required.")]
        public SpeciesEnum Species { get; set; }

        [Required(ErrorMessage = "Animal's date of birth is required.")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Animal's gender is required.")]
        public GenderEnum Gender { get; set; }

        [Required(ErrorMessage = "Owner's id is required.")]
        public int OwnerId { get; set; }

    }
}
