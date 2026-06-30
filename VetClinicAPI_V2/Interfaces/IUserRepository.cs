using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Params;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IUserRepository 
    {
        Task<ICollection<UserResponseDTO>> GetAllUsersAsync(UserQueryParameters queryParameters);
        Task<UserResponseDTO?> GetUserByIdAsync(int id);
        Task<UserResponseDTO?> GetUserByEmailAsync(string email);
        Task<UserResponseDTO> CreateUserAsync(UserRegisterDTO userRegister);
        Task<UserResponseDTO?> UpdateUserAsync(UserUpdateDTO userUpdate);
        Task<bool> DeleteUserAsync(int id);
    }
}
