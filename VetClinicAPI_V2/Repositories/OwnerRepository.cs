using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ClinicDbContext _context;

        public OwnerRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<OwnerResponseDTO> CreateOwnerAsync(OwnerCreateDTO ownerCreate)
        {
            var newOwner = new Owner
            {
                Name = ownerCreate.Name,
                Phone = ownerCreate.Phone,
                IsDeleted = false
            };

            _context.Owners.Add(newOwner);

            await _context.SaveChangesAsync();

            return new OwnerResponseDTO {
                Id = newOwner.Id,
                Name = newOwner.Name,
                Phone = newOwner.Phone,
                Animals = new List<AnimalSummaryDTO>()
            };
        }

        public async Task<bool> DeleteOwnerAsync(int id)
        {
            var existingOwner = await _context.Owners
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOwner == null)
            {
                return false;
            }

            existingOwner.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<ICollection<OwnerResponseDTO>> GetAllOwnersAsync(string? search = null)
        {
            var query = _context.Owners.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o => o.Name.Contains(search) || o.Phone.Contains(search));
            }

            return await query
                .Select(o => new OwnerResponseDTO {
                    Id = o.Id,
                    Name = o.Name,
                    Phone = o.Phone,
                    Animals = o.Animals
                        .Select(a => new AnimalSummaryDTO {
                            Id = a.Id,
                            Code = a.Code,
                            Name = a.Name,
                            Species = a.Species,
                            DateOfBirth = a.DateOfBirth,
                            Gender = a.Gender
                        }).ToList()
                }).ToListAsync();
        }

        public async Task<ICollection<AnimalResponseDTO>> GetOwnerAnimalsAsync(int id)
        {
            return await _context.Animals
                .AsNoTracking()
                .Include(a => a.Owner)
                .Where(a => a.OwnerId == id)
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

        public async Task<OwnerResponseDTO?> GetOwnerByIdAsync(int id)
        {
            return await _context.Owners
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Select(o => new OwnerResponseDTO {
                    Id=o.Id,
                    Name = o.Name,
                    Phone = o.Phone,
                    Animals = o.Animals
                    .Select(a => new AnimalSummaryDTO {
                        Id = a.Id,
                        Name=a.Name,
                        Code = a.Code,
                        Species = a.Species,
                        DateOfBirth=a.DateOfBirth,
                        Gender=a.Gender
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<OwnerResponseDTO?> UpdateOwnerAsync(OwnerUpdateDTO ownerUpdate)
        {
            var existingOwner = await _context.Owners
                .FirstOrDefaultAsync(o => o.Id == ownerUpdate.Id);

            if (existingOwner == null)
            {
                return null;
            }

            existingOwner.Name = ownerUpdate.Name;
            existingOwner.Phone = ownerUpdate.Phone;

            await _context.SaveChangesAsync();

            return new OwnerResponseDTO
            {
                Id = existingOwner.Id,
                Name = existingOwner.Name,
                Phone = existingOwner.Phone,
                Animals = new List<AnimalSummaryDTO>()
            };
        }
    }
}
