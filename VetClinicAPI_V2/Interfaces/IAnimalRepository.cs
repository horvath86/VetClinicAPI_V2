using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IAnimalRepository
    {
        Task<ICollection<AnimalResponseDTO>> GetAllAnimalsAsync(string? search = null, SpeciesEnum? species = null);
        Task<AnimalResponseDTO?> GetAnimalByIdAsync(int id);
        Task<AnimalResponseDTO> CreateAnimalAsync(AnimalCreateDTO createAnimal);
        Task<AnimalResponseDTO?> UpdateAnimalAsync(AnimalUpdateDTO updateAnimal);
        Task<bool> DeleteAnimalAsync(int id);
    }
}
