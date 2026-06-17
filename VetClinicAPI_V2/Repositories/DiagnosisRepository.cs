using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Repositories
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly ClinicDbContext _context;

        public DiagnosisRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Diagnosis> CreateDiagnosisAsync(DiagnosisCreateDTO createDiagnosis)
        {
            var newDiagnosis = new Diagnosis {
                Code = createDiagnosis.Code,
                Name = createDiagnosis.Name
            };

            _context.Add(newDiagnosis);

            await _context.SaveChangesAsync();

            return newDiagnosis;
        }

        public async Task<bool> DeleteDiagnosisAsync(int id)
        {
            var existingDiagnosis = await _context.Diagnoses
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (existingDiagnosis == null)
            {
                return false;
            }

            existingDiagnosis.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Diagnosis>> GetAllDiagnosesAsync(string? search)
        {
            var query = _context.Diagnoses.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d => d.Code.Contains(search) || d.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Diagnosis?> GetDiagnosisByIdAsync(int id)
        {
            var existingDiagnosis = await _context.Diagnoses
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (existingDiagnosis == null)
            {
                return null;
            }

            return existingDiagnosis;
        }

        public async Task<Diagnosis?> UpdateDiagnosisAsync(DiagnosisUpdateDTO updateDiagnosis)
        {
            var existingDiagnosis = await _context.Diagnoses
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.Id == updateDiagnosis.Id);

            if (existingDiagnosis == null)
            {
                return null;
            }

            existingDiagnosis.Code = updateDiagnosis.Code;
            existingDiagnosis.Name = updateDiagnosis.Name;

            await _context.SaveChangesAsync();

            return existingDiagnosis;
        }
    }
}
