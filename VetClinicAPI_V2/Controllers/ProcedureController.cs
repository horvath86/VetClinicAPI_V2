using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureController(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        [HttpGet(Name = "GetProcedures")]
        public async Task<ActionResult<ICollection<Procedure>>> GetProcedures([FromQuery] string? search)
        {
            var result = await _procedureRepository.GetAllProceduresAsync(search);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetProcedureById")]
        public async Task<ActionResult<Procedure>> GetProcedureById(int id)
        {
            var result = await _procedureRepository.GetProcedureByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost(Name = "CreateProcedure")]
        public async Task<ActionResult<Procedure>> CreateProcedure([FromBody] ProcedureCreateDTO dto)
        {
            var result = await _procedureRepository.CreateProcedureAsync(dto);

            return CreatedAtAction(nameof(GetProcedureById), new { id = result.Id }, result);
        }

        [HttpPut(Name = "UpdateProcedure")]
        public async Task<ActionResult<Procedure>> UpdateProcedure([FromBody] ProcedureUpdateDTO dto)
        {
            var result = await _procedureRepository.UpdateProcedureAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteProcedure")]
        public async Task<ActionResult<bool>> DeleteProcedure(int id)
        {
            var deleted = await _procedureRepository.DeleteProcedureAsync(id);

            if (!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
    }
}
