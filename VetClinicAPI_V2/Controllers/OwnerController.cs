using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Interfaces;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<OwnerResponseDTO>>> GetOwners([FromQuery] string? search)
        {
            var result = await _ownerRepository.GetAllOwnersAsync(search);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerResponseDTO>> GetOwnerById(int id)
        {
            var result = await _ownerRepository.GetOwnerByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OwnerResponseDTO>> CreateOwner([FromBody] OwnerCreateDTO dto)
        {
            var result = await _ownerRepository.CreateOwnerAsync(dto);

            return CreatedAtAction(nameof(GetOwnerById), new {id = result.Id}, result);
        }

        [HttpPut]
        public async Task<ActionResult<OwnerResponseDTO>> UpdateOwner([FromBody] OwnerUpdateDTO dto)
        {
            var result = await _ownerRepository.UpdateOwnerAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteOwner(int id)
        {
            var deleted = await _ownerRepository.DeleteOwnerAsync(id);

            if (!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
    }
}
