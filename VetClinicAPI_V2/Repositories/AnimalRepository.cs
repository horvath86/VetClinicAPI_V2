using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Enums;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ClinicDbContext _context;

        public AnimalRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<AnimalResponseDTO> CreateAnimalAsync(AnimalCreateDTO createAnimal)
        {
            var newAnimal = new Animal {
                Code = createAnimal.Code,
                Name = createAnimal.Name,
                Species = createAnimal.Species,
                DateOfBirth = createAnimal.DateOfBirth,
                Gender = createAnimal.Gender,
                OwnerId = createAnimal.OwnerId
            };

            _context.Animals.Add(newAnimal);

            await _context.SaveChangesAsync();

            var linkedOwner = await _context.Owners.AsNoTracking().FirstOrDefaultAsync(o => o.Id == newAnimal.OwnerId);

            return new AnimalResponseDTO {
                Id = newAnimal.Id,
                Code = newAnimal.Code,
                Name = newAnimal.Name,
                Species = newAnimal.Species,
                DateOfBirth= newAnimal.DateOfBirth,
                Gender = newAnimal.Gender,
                OwnerId = newAnimal.OwnerId,
                OwnerName = linkedOwner != null ? linkedOwner.Name : string.Empty,
                OwnerPhone = linkedOwner != null ? linkedOwner.Phone : string.Empty
            }; 
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var existingAnimal = await _context.Animals.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Id == id);

            if (existingAnimal == null)
            {
                return false;
            }

            existingAnimal.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<AnimalResponseDTO>> GetAllAnimalsAsync(string? search = null, SpeciesEnum? species = null)
        {
            var query = _context.Animals.Include(a => a.Owner).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a => a.Name.Contains(search) || a.Code.Contains(search));
            }

            if (species.HasValue)
            {
                query = query.Where(a => a.Species == species.Value);
            }

            return await query
                .Select(a => new AnimalResponseDTO {
                    Id = a.Id,
                    Code = a.Code,
                    Name = a.Name,
                    Species = a.Species,
                    DateOfBirth = a.DateOfBirth,
                    Gender = a.Gender,

                    OwnerId = a.OwnerId,
                    OwnerName = a.Owner != null ? a.Owner.Name : string.Empty,
                    OwnerPhone = a.Owner != null ? a.Owner.Phone : string.Empty
                }).ToListAsync();
        }

        public async Task<AnimalResponseDTO?> GetAnimalByIdAsync(int id)
        {
            var existingAnimal = await _context.Animals
                .Include(a => a.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existingAnimal == null)
            {
                return null;
            }

            return new AnimalResponseDTO {
                Id = existingAnimal.Id,
                Code = existingAnimal.Code,
                Name = existingAnimal.Name,
                Species = existingAnimal.Species,
                DateOfBirth = existingAnimal.DateOfBirth,
                Gender = existingAnimal.Gender,
                OwnerId= existingAnimal.OwnerId,

                OwnerName = existingAnimal.Owner != null ? existingAnimal.Owner.Name : string.Empty,
                OwnerPhone = existingAnimal.Owner != null ? existingAnimal.Owner.Phone: string.Empty
            };


        }

        public async Task<AnimalResponseDTO?> UpdateAnimalAsync(AnimalUpdateDTO updateAnimal)
        {
            var existingAnimal = await _context.Animals
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == updateAnimal.Id);

            if (existingAnimal == null)
            {
                return null;
            }

            existingAnimal.Code = updateAnimal.Code;
            existingAnimal.Name = updateAnimal.Name;
            existingAnimal.Species = updateAnimal.Species;
            existingAnimal.DateOfBirth = updateAnimal.DateOfBirth;
            existingAnimal.Gender = updateAnimal.Gender;
            existingAnimal.OwnerId = updateAnimal.OwnerId;
            
            await _context.SaveChangesAsync();

            return new AnimalResponseDTO
            {
                Id = existingAnimal.Id,
                Code = existingAnimal.Code,
                Name = existingAnimal.Name,
                Species = existingAnimal.Species,
                DateOfBirth = existingAnimal.DateOfBirth,
                Gender = existingAnimal.Gender,
                OwnerId = existingAnimal.OwnerId,

                OwnerName = existingAnimal.Owner != null ? existingAnimal.Owner.Name : string.Empty,
                OwnerPhone = existingAnimal.Owner != null ? existingAnimal.Owner.Phone : string.Empty
            };
        }
    }
}
