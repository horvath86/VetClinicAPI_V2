using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Enums;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Params;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<ICollection<UserResponseDTO>>> GetUsers([FromQuery] UserQueryParameters queryParameters)
        {
            var result = await _userRepository.GetAllUsersAsync(queryParameters);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserResponseDTO?>> GetUserById(int id)
        {
            var result = await _userRepository.GetUserByIdAsync(id);

            if (result == null)
            {
                return NotFound(null);
            }

            return Ok(result);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult<UserResponseDTO?>> UpdateUser([FromBody] UserUpdateDTO dto)
        {
            var result = await _userRepository.UpdateUserAsync(dto);

            if (result == null)
            {
                return NotFound(null);
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var deleted = await _userRepository.DeleteUserAsync(id);

            if (!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
        
    }
}
