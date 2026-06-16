using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IOwnerRepository
    {
        Task<ICollection<OwnerResponseDTO>> GetAllOwnersAsync(string? search = null);
        Task<OwnerResponseDTO?> GetOwnerByIdAsync(int id);
        Task<OwnerResponseDTO> CreateOwnerAsync(OwnerCreateDTO ownerCreate);
        Task<OwnerResponseDTO?> UpdateOwnerAsync(OwnerUpdateDTO ownerUpdate);
        Task<ICollection<AnimalResponseDTO>> GetOwnerAnimalsAsync(int id);
        Task<bool> DeleteOwnerAsync(int id);
    }
}
