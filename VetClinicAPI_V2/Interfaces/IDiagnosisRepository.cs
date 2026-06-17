using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IDiagnosisRepository
    {
        Task<ICollection<Diagnosis>> GetAllDiagnosesAsync(string? search);
        Task<Diagnosis?> GetDiagnosisByIdAsync(int id);
        Task<Diagnosis> CreateDiagnosisAsync(DiagnosisCreateDTO createDiagnosis);
        Task<Diagnosis?> UpdateDiagnosisAsync(DiagnosisUpdateDTO updateDiagnosis);
        Task<bool> DeleteDiagnosisAsync(int id);
    }
}
