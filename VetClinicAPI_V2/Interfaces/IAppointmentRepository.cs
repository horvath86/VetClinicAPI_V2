using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<ICollection<AppointmentResponseDTO>> GetAllAppointmentsAsync(string? search, DateOnly? dateOnly);
        Task<AppointmentResponseDTO?> GetAppointmentByIdAsync(int id);
        Task<AppointmentResponseDTO> CreateAppointmentAsync(AppointmentCreateDTO createAppointment);
        Task<AppointmentResponseDTO?> UpdateAppointmentAsync(AppointmentUpdateDTO updateAppointment);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
