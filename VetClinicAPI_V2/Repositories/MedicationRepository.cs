using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Repositories
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly ClinicDbContext _context;

        public MedicationRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Medication> CreateMedicationAsync(MedicationCreateDTO createMedication)
        {
            var newMedication = new Medication {
                Code = createMedication.Code,
                Name = createMedication.Name,
                Description = createMedication.Description
            };

            _context.Medications.Add(newMedication);

            await _context.SaveChangesAsync();

            return newMedication;
        }

        public async Task<bool> DeleteMedicationAsync(int id)
        {
            var existingMedication = await _context.Medications
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMedication == null)
            {
                return false;
            }

            existingMedication.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Medication>> GetAllMedicationAsync(string? search)
        {
            var query = _context.Medications.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.Code.Contains(search) || m.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Medication?> GetMedicationByIdAsync(int id)
        {
            var existingMedication = await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMedication == null)
            {
                return null;
            }

            return existingMedication;
        }

        public async Task<Medication?> UpdateMedicationAsync(MedicationUpdateDTO updateMedication)
        {
            var existingMedication = await _context.Medications
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(m => m.Id == updateMedication.Id);

            if (existingMedication == null)
            {
                return null;
            }

            existingMedication.Code = updateMedication.Code;
            existingMedication.Name = updateMedication.Name;
            existingMedication.Description = updateMedication.Description;

            await _context.SaveChangesAsync();

            return existingMedication;
        }
    }
}
