using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Enums;
using VetClinicAPI_V2.Interfaces;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<AnimalResponseDTO>>> GetAnimals([FromQuery] string? search, [FromQuery] SpeciesEnum? species)
        {
            var result = await _animalRepository.GetAllAnimalsAsync(search, species);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalResponseDTO>> GetAnimalById(int id)
        {
            var result = await _animalRepository.GetAnimalByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AnimalResponseDTO>> CreateAnimal([FromBody] AnimalCreateDTO dto)
        {
            var result = await _animalRepository.CreateAnimalAsync(dto);

            return CreatedAtAction(nameof(GetAnimalById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult<AnimalResponseDTO>> UpdateAnimal([FromBody] AnimalUpdateDTO dto)
        {
            var result = await _animalRepository.UpdateAnimalAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAnimal(int id)
        {
            var deleted = await _animalRepository.DeleteAnimalAsync(id);

            if(!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
    }
}
