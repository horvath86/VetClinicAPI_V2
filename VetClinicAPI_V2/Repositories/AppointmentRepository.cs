using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicDbContext _context;

        public AppointmentRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentResponseDTO> CreateAppointmentAsync(AppointmentCreateDTO createAppointment)
        {
            var newAppointment = new Appointment {
                OwnerName = createAppointment.OwnerName,
                Phone = createAppointment.Phone,
                AnimalName = createAppointment.AnimalName,
                UserId = createAppointment.UserId,
                ScheduledDateTime = createAppointment.ScheduledDateTime,
                Status = createAppointment.Status
            };

            _context.Appointments.Add(newAppointment);

            await _context.SaveChangesAsync();

            var linkeduser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == newAppointment.UserId);

            return new AppointmentResponseDTO
            {
                Id = newAppointment.Id,
                OwnerName = newAppointment.OwnerName,
                Phone = newAppointment.Phone,
                AnimalName = newAppointment.AnimalName,
                UserId = newAppointment.UserId,
                VeterinarianName = linkeduser != null ? linkeduser.Name : string.Empty,
                ScheduledDateTime = newAppointment.ScheduledDateTime,
                Status = newAppointment.Status
            };

            
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var existingAppointment = await _context.Appointments
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existingAppointment == null)
            {
                return false;
            }

            existingAppointment.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<AppointmentResponseDTO>> GetAllAppointmentsAsync(string? search, DateOnly? dateOnly)
        {
            var query = _context.Appointments.Include(a => a.User).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a => a.OwnerName.Contains(search) || a.Phone.Contains(search));
            }

            if (dateOnly.HasValue)
            {
                DateTime startOfDay = dateOnly.Value.ToDateTime(TimeOnly.MinValue);
                DateTime endOfDay = dateOnly.Value.ToDateTime(TimeOnly.MaxValue);

                query = query.Where(a => a.ScheduledDateTime >= startOfDay && a.ScheduledDateTime <= endOfDay);
            }

            return await query
                .Select(a => new AppointmentResponseDTO {
                    Id = a.Id,
                    OwnerName = a.OwnerName,
                    Phone = a.Phone,
                    AnimalName = a.AnimalName,
                    UserId = a.UserId,
                    VeterinarianName = a.User != null ? a.User.Name : string.Empty,
                    ScheduledDateTime = a.ScheduledDateTime,
                    Status = a.Status
                }).ToListAsync();
        }

        public async Task<AppointmentResponseDTO?> GetAppointmentByIdAsync(int id)
        {
            var existingAppointment = await _context.Appointments
                .Include(a => a.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existingAppointment == null)
            {
                return null;
            }

            return new AppointmentResponseDTO
            {
                Id = existingAppointment.Id,
                OwnerName = existingAppointment.OwnerName,
                Phone = existingAppointment.Phone,
                AnimalName = existingAppointment.AnimalName,
                UserId = existingAppointment.UserId,
                VeterinarianName = existingAppointment.User != null ? existingAppointment.User.Name : string.Empty,
                ScheduledDateTime = existingAppointment.ScheduledDateTime,
                Status = existingAppointment.Status
            };
        }

        public async Task<AppointmentResponseDTO?> UpdateAppointmentAsync(AppointmentUpdateDTO updateAppointment)
        {
            var existingAppointment = await _context.Appointments
                .IgnoreQueryFilters()
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == updateAppointment.Id);

            if (existingAppointment == null)
            {
                return null;
            }

            existingAppointment.OwnerName = updateAppointment.OwnerName;
            existingAppointment.Phone = updateAppointment.Phone;
            existingAppointment.AnimalName = updateAppointment.AnimalName;
            existingAppointment.UserId = updateAppointment.UserId;
            existingAppointment.ScheduledDateTime = updateAppointment.ScheduledDateTime;
            existingAppointment.Status = updateAppointment.Status;

            await _context.SaveChangesAsync();

            return new AppointmentResponseDTO
            {
                Id = existingAppointment.Id,
                OwnerName = existingAppointment.OwnerName,
                Phone = existingAppointment.Phone,
                AnimalName = existingAppointment.AnimalName,
                UserId = existingAppointment.UserId,
                VeterinarianName = existingAppointment.User != null ? existingAppointment.User.Name : string.Empty,
                ScheduledDateTime = existingAppointment.ScheduledDateTime,
                Status = existingAppointment.Status
            };

        }
    }
}
