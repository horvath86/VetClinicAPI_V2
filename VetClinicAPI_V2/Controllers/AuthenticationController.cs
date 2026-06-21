using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Interfaces;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register", Name = "Register")]
        public async Task<ActionResult<UserResponseDTO>> Register([FromBody] UserRegisterDTO dto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                return BadRequest("User with that email is already registered");
            }

            var result = await _userRepository.CreateUserAsync(dto);

            return CreatedAtRoute("GetUserById", new { id = result.Id }, result);
        }

        [HttpPost("login", Name = "Login")]
        public async Task<ActionResult<UserResponseDTO>> Login([FromBody] UserLoginDTO dto)
        {
            var result = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (result == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(result);
        }
    }
}
