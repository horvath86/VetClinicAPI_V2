using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Interfaces
{
    public interface IProcedureRepository
    {
        Task<ICollection<Procedure>> GetAllProceduresAsync(string? search);
        Task<Procedure?> GetProcedureByIdAsync(int id);
        Task<Procedure> CreateProcedureAsync(ProcedureCreateDTO createProcedure);
        Task<Procedure?> UpdateProcedureAsync(ProcedureUpdateDTO updateProcedure);
        Task<bool> DeleteProcedureAsync(int id);
    }
}
