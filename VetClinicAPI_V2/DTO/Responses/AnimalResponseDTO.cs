using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.DTO.Responses
{
    public class AnimalResponseDTO
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public SpeciesEnum Species { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public GenderEnum Gender { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; } = string.Empty;

        public string OwnerPhone { get; set; } = string.Empty;
    }
}
