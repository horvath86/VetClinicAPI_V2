

namespace VetClinicAPI_V2.DTO.Responses
{
    public class ProcedureRecordSummaryDTO
    {
        public int Id { get; set; }

        public int ProcedureId { get; set; }

        public string ProcedureCode { get; set; } = string.Empty;

        public string ProcedureName { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public int DurationInMinutes { get; set; }

        public string? AnesthesiaUsed { get; set; }
    }
}
