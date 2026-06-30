using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Enums;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;
using VetClinicAPI_V2.Params;

namespace VetClinicAPI_V2.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ClinicDbContext _context;

        public UserRepository(ClinicDbContext Context)
        {
            _context = Context;
        }

        public async Task<UserResponseDTO> CreateUserAsync(UserRegisterDTO userRegister)
        {
            var newUserEntity = new User
            {
                Name = userRegister.Name,
                Phone = userRegister.Phone,
                Email = userRegister.Email,
                LicenceNumber = userRegister.LicenceNumber,
                PassHash = BCrypt.Net.BCrypt.HashPassword(userRegister.Password),
                Role = userRegister.Role,
                IsActive = true

            };

            _context.Users.Add(newUserEntity);

            await  _context.SaveChangesAsync();

            return new UserResponseDTO
            {
                Id = newUserEntity.Id,
                Name = newUserEntity.Name,
                Phone = newUserEntity.Phone,
                Email = newUserEntity.Email,
                LicenceNumber = newUserEntity.LicenceNumber,
                Role = newUserEntity.Role,
                IsActive = newUserEntity.IsActive
            };
        }

        //soft delete
        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users
                //enable finding soft deleted entities
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }

        //get all or by role
        public async Task<ICollection<UserResponseDTO>> GetAllUsersAsync(UserQueryParameters queryParameters) 
        {
            var query = _context.Users.AsNoTracking();

            if (queryParameters.Role.HasValue)
            {
                query = query.Where(u => u.Role == queryParameters.Role.Value);
            }

            if (queryParameters.includeDeleted == true)
            {
                query = query.IgnoreQueryFilters();
            }

            return await query
                .Select(u => new UserResponseDTO {
                    Id = u.Id,
                    Name = u.Name,
                    Phone = u.Phone,
                    Email = u.Email,
                    LicenceNumber = u.LicenceNumber,
                    Role = u.Role,
                    IsActive = u.IsActive
                }).ToListAsync();
        }

        public async Task<UserResponseDTO?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking()
                //enable finding soft deleted entities
                .IgnoreQueryFilters()
                .Where(u => u.Email == email)
                .Select(u => new UserResponseDTO {
                    Id = u.Id,
                    Name = u.Name,
                    Phone = u.Phone,
                    Email = u.Email,
                    LicenceNumber = u.LicenceNumber,
                    Role = u.Role,
                    IsActive = u.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => new UserResponseDTO {
                    Id = u.Id,
                    Name = u.Name,
                    Phone = u.Phone,
                    Email = u.Email,
                    LicenceNumber = u.LicenceNumber,
                    Role = u.Role,
                    IsActive = u.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<UserResponseDTO?> UpdateUserAsync(UserUpdateDTO userUpdate)
        {
            var existingUser = await _context.Users
                //enable finding soft deleted entities
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == userUpdate.Id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Name = userUpdate.Name;
            existingUser.Phone = userUpdate.Phone;
            existingUser.Email = userUpdate.Email;
            existingUser.LicenceNumber = userUpdate.LicenceNumber;
            existingUser.Role = userUpdate.Role;
            existingUser.IsActive = userUpdate.IsActive;

            await _context.SaveChangesAsync();

            return new UserResponseDTO
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Phone= existingUser.Phone,
                Email= existingUser.Email,
                LicenceNumber= existingUser.LicenceNumber,
                Role= existingUser.Role,
                IsActive= existingUser.IsActive
            };
        }
    }
}
