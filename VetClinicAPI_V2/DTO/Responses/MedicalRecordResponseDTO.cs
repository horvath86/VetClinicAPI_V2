using VetClinicAPI_V2.Enums;


namespace VetClinicAPI_V2.DTO.Responses
{
    public class MedicalRecordResponseDTO
    {
        public int Id { get; set; }

        public DateOnly VisitDate { get; set; }

        public string Symptoms { get; set; } = string.Empty;

        public string? Notes { get; set; }


        public ICollection<PrescriptionSummaryDTO> Prescriptions { get; set; } = new List<PrescriptionSummaryDTO>();

        public ICollection<ProcedureRecordSummaryDTO> ProcedureRecords { get; set; } = new List<ProcedureRecordSummaryDTO>();


        public int AnimalId { get; set; }

        public string AnimalCode { get; set; } = string.Empty;

        public string AnimalName { get; set; } = string.Empty;

        public SpeciesEnum AnimalSpecies { get; set; }


        public int UserId { get; set; }

        public string VeterinarianName { get; set; } = string.Empty;


        public int DiagnosisId { get; set; }

        public string DiagnosisCode { get; set; } = string.Empty;

        public string DiagnosisName { get; set; } = string.Empty;

    }
}
