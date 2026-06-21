using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationController(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        [HttpGet(Name = "GetMedications")]
        public async Task<ActionResult<ICollection<Medication>>> GetMedications([FromQuery] string? search)
        {
            var result = await _medicationRepository.GetAllMedicationsAsync(search);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetMedicationById")]
        public async Task<ActionResult<Medication>> GetMedicationById(int id)
        {
            var result = await _medicationRepository.GetMedicationByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost(Name = "CreateMedication")]
        public async Task<ActionResult<Medication>> CreateMedication([FromBody] MedicationCreateDTO dto)
        {
            var result = await _medicationRepository.CreateMedicationAsync(dto);

            return CreatedAtAction(nameof(GetMedicationById), new { id = result.Id }, result);
        }

        [HttpPut(Name = "UpdateMedication")]
        public async Task<ActionResult<Medication>> UpdateMedication([FromBody] MedicationUpdateDTO dto)
        {
            var result = await _medicationRepository.UpdateMedicationAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteMedication")]
        public async Task<ActionResult<bool>> DeleteMedication(int id)
        {
            var deleted = await _medicationRepository.DeleteMedicationAsync(id);

            if (!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
    }
}
