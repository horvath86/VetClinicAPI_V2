using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IAnimalInterface
    {
        public Task<ICollection<AnimalResponseDTO>> GetAllAnimals(string? search = null);
        public Task<AnimalResponseDTO?> GetAnimalById(int id);
        public Task<AnimalResponseDTO> CreateAnimal(AnimalCreateDTO createAnimal);
        public Task<AnimalResponseDTO?> UpdateAnimal(AnimalUpdateDTO updateAnimal);
        public Task<bool> DeleteAnimal(int id);
    }
}
