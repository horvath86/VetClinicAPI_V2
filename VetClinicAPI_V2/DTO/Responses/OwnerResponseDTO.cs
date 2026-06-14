using System.ComponentModel.DataAnnotations;

namespace VetClinicAPI_V2.DTO.Responses
{
    public class OwnerResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public ICollection<AnimalSummaryDTO> Animals { get; set; } = new List<AnimalSummaryDTO>();

    }
}
