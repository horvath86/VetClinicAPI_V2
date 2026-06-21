using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisRepository _diagnosisRepository;

        public DiagnosisController(IDiagnosisRepository diagnosisRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }

        [HttpGet(Name = "GetDiagnoses")]
        public async Task<ActionResult<ICollection<Diagnosis>>> GetDiagnoses([FromQuery] string? search)
        {
            var result = await _diagnosisRepository.GetAllDiagnosesAsync(search);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetDiagnosisById")]
        public async Task<ActionResult<Diagnosis>> GetDiagnosisById(int id)
        {
            var result = await _diagnosisRepository.GetDiagnosisByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost(Name = "CreateDignosis")]
        public async Task<ActionResult<Diagnosis>> CreateDiagnosis([FromBody] DiagnosisCreateDTO dto)
        {
            var result = await _diagnosisRepository.CreateDiagnosisAsync(dto);

            return CreatedAtAction(nameof(GetDiagnosisById), new { id = result.Id }, result);
        }

        [HttpPut(Name = "UpdateDiagnosis")]
        public async Task<ActionResult<Diagnosis>> UpdateDiagnosis([FromBody] DiagnosisUpdateDTO dto)
        {
            var result = await _diagnosisRepository.UpdateDiagnosisAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteDiagnosis")]
        public async Task<ActionResult<bool>> DeleteDiagnosis(int id)
        {
            var deleted = await _diagnosisRepository.DeleteDiagnosisAsync(id);

            if (!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
    }
}
