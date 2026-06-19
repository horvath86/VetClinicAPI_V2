using Microsoft.AspNetCore.Mvc;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Interfaces;

namespace VetClinicAPI_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<AppointmentResponseDTO>>> GetAppointments([FromQuery] string? search, [FromQuery] DateOnly? dateOnly)
        {
            var result = await _appointmentRepository.GetAllAppointmentsAsync(search, dateOnly);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentResponseDTO>> GetAppointmentById(int id)
        {
            var result = await _appointmentRepository.GetAppointmentByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentResponseDTO>> CreateAppointment([FromBody] AppointmentCreateDTO dto)
        {
            var result = await _appointmentRepository.CreateAppointmentAsync(dto);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult<AppointmentResponseDTO>> UpdateAppointment([FromBody] AppointmentUpdateDTO dto)
        {
            var result = await _appointmentRepository.UpdateAppointmentAsync(dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAppointment(int id)
        {
            var deleted = await _appointmentRepository.DeleteAppointmentAsync(id);

            if (!deleted)
            {
                return NotFound(false);
            }

            return Ok(true);
        }
    }
}
