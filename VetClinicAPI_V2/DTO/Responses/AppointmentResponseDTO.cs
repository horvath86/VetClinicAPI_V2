using VetClinicAPI_V2.Enums;

namespace VetClinicAPI_V2.DTO.Responses
{
    public class AppointmentResponseDTO
    {
        public int Id { get; set; }

        public string OwnerName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string AnimalName { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string VeterinarianName { get; set; } = string.Empty;

        public DateTime ScheduledDateTime { get; set; }

        public StatusEnum Status { get; set; }
    }
}
