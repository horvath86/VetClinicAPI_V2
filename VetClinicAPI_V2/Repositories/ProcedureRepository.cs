using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {
        private readonly ClinicDbContext _context;

        public ProcedureRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Procedure> CreateProcedureAsync(ProcedureCreateDTO createProcedure)
        {
            var newProcedure = new Procedure {
                Name = createProcedure.Name,
                Code = createProcedure.Code
            };

            _context.Procedures.Add(newProcedure);

            await _context.SaveChangesAsync();

            return newProcedure;
        }

        public async Task<bool> DeleteProcedureAsync(int id)
        {
            var existingProcedure = await _context.Procedures
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProcedure == null)
            {
                return false;
            }

            existingProcedure.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Procedure>> GetAllProceduresAsync(string? search)
        {
            var query = _context.Procedures
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Code.Contains(search) || p.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Procedure?> GetProcedureByIdAsync(int id)
        {
            var existingProcedure = await _context.Procedures
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProcedure == null)
            {
                return null;
            }

            return existingProcedure;
        }

        public async Task<Procedure?> UpdateProcedureAsync(ProcedureUpdateDTO updateProcedure)
        {
            var existingProcedure = await _context.Procedures
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == updateProcedure.Id);

            if (existingProcedure == null)
            {
                return null;
            }

            existingProcedure.Code = updateProcedure.Code;
            existingProcedure.Name = updateProcedure.Name;

            await _context.SaveChangesAsync();

            return existingProcedure;
        }
    }
}
