using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Interfaces;

namespace VetClinicAPI_V2.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordController(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<MedicalRecordResponseDTO>>> GetMedicalRecords([FromQuery] DateOnly? dateOnly, [FromQuery] int? userId, [FromQuery] int? animalId)
        {
            var result = await _medicalRecordRepository.GetAllMedicalRecordsAsync(dateOnly, userId, animalId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordResponseDTO?>> GetMedicalRecordById(int id)
        {
            var result = await _medicalRecordRepository.GetMedicalRecordByIdAsync(id);

            if (result == null)
            {
                return NotFound(null);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecordResponseDTO>> CreateMedicalRecord([FromBody] MedicalRecordCreateDTO dto)
        {
            var result = await _medicalRecordRepository.CreateMedicalRecordAsync(dto);

            return CreatedAtAction(nameof(GetMedicalRecordById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult<MedicalRecordResponseDTO?>> UpdateMedicalRecord([FromBody] MedicalRecordUpdateDTO dto)
        {
            var result = await _medicalRecordRepository.UpdateMedicalRecordAsync(dto);

            if (result == null)
            {
                return NotFound(null);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMedicalRecord(int id) 
        {
            var deleted = await _medicalRecordRepository.DeleteMedicalRecordAsync(id);

            if (!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
    }
}
