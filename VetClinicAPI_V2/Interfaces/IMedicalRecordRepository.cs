using System.Runtime.CompilerServices;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<ICollection<MedicalRecordResponseDTO>> GetAllMedicalRecordsAsync(DateOnly? dateOnly = null, int? userId = null, int? animalid = null);
        Task<MedicalRecordResponseDTO?> GetMedicalRecordByIdAsync(int id);
        Task<MedicalRecordResponseDTO> CreateMedicalRecordAsync(MedicalRecordCreateDTO createMedicalRecord);
        Task<MedicalRecordResponseDTO?> UpdateMedicalRecordAsync(MedicalRecordUpdateDTO updateMedicalRecord);
        Task<bool> DeleteMedicalRecordAsync(int id);
    }
}
