
namespace VetClinicAPI_V2.DTO.Responses
{
    public class PrescriptionSummaryDTO
    {

        public int Id { get; set; }

        public int MedicationId { get; set; }

        public string MedicationCode { get; set; } = string.Empty;

        public string MedicationName { get; set; } = string.Empty;

        public string DosageInstructions { get; set; } = string.Empty;
      
    }
}
