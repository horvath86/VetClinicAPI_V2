using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IMedicationRepository
    {
        Task<ICollection<Medication>> GetAllMedicationsAsync(string? search);
        Task<Medication?> GetMedicationByIdAsync(int id);
        Task<Medication> CreateMedicationAsync(MedicationCreateDTO createMedication);
        Task<Medication?> UpdateMedicationAsync(MedicationUpdateDTO updateMedication);
        Task<bool> DeleteMedicationAsync(int id);
    }
}
