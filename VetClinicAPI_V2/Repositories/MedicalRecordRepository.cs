using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Data;
using VetClinicAPI_V2.DTO.Requests;
using VetClinicAPI_V2.DTO.Responses;
using VetClinicAPI_V2.Interfaces;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ClinicDbContext _context;

        public MedicalRecordRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalRecordResponseDTO> CreateMedicalRecordAsync(MedicalRecordCreateDTO createMedicalRecord)
        {
            var newMedicalRecord = new MedicalRecord
            {
                AnimalId = createMedicalRecord.AnimalId,
                UserId = createMedicalRecord.UserId,
                DiagnosisId = createMedicalRecord.DiagnosisId,
                VisitDate = createMedicalRecord.VisitDate,
                Symptoms = createMedicalRecord.Symptoms,
                Notes = createMedicalRecord.Notes,
                IsDeleted = false
            };

            newMedicalRecord.Prescriptions = createMedicalRecord.Prescriptions.Select(p => new Prescription
            {
                MedicationId = p.MedicationId,
                DosageInstructions = p.DosageInstructions,
            }).ToList();

            newMedicalRecord.ProcedureRecords = createMedicalRecord.ProcedureRecords.Select(p => new ProcedureRecord {
                ProcedureId = p.ProcedureId,
                Notes= p.Notes,
                DurationInMinutes = p.DurationInMinutes,
                AnesthesiaUsed = p.AnesthesiaUsed
            }).ToList();


            _context.MedicalRecords.Add(newMedicalRecord);

            await _context.SaveChangesAsync();

            return (await GetMedicalRecordByIdAsync(newMedicalRecord.Id))!;
        }

        public async Task<bool> DeleteMedicalRecordAsync(int id)
        {
            var existingMedicalRecord = await _context.MedicalRecords
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMedicalRecord == null)
            {
                return false;
            }

            existingMedicalRecord.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<MedicalRecordResponseDTO>> GetAllMedicalRecordsAsync(DateOnly? dateOnly = null, int? userId = null, int? animalid = null)
        {
            var query = _context.MedicalRecords.AsNoTracking();

            if (dateOnly.HasValue)
            {
                query = query.Where(m => m.VisitDate == dateOnly.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(m => m.UserId == userId.Value);
            }

            if (animalid.HasValue)
            {
                query = query.Where(m => m.AnimalId == animalid.Value);
            }

            return await query
                .Select(m => new MedicalRecordResponseDTO { 
                    Id = m.Id,
                    VisitDate = m.VisitDate,
                    Symptoms = m.Symptoms,
                    Notes = m.Notes,

                    //flatten data
                    AnimalId = m.AnimalId,
                    AnimalCode = m.Animal != null ? m.Animal.Code : string.Empty,
                    AnimalName = m.Animal != null ? m.Animal.Name : string.Empty,
                    AnimalSpecies = m.Animal != null ? m.Animal.Species : Enums.SpeciesEnum.Canine,

                    UserId = m.UserId,
                    VeterinarianName = m.User != null ? m.User.Name : string.Empty,

                    DiagnosisId = m.DiagnosisId,
                    DiagnosisCode = m.Diagnosis != null ? m.Diagnosis.Code : string.Empty,
                    DiagnosisName = m.Diagnosis != null ? m.Diagnosis.Name : string.Empty,

                    Prescriptions = m.Prescriptions.Select(p => new PrescriptionSummaryDTO { 
                        Id = p.Id,
                        MedicationId = p.MedicationId,
                        MedicationName = p.Medication != null ? p.Medication.Name : string.Empty,
                    }).ToList(),

                    ProcedureRecords = m.ProcedureRecords.Select(p => new ProcedureRecordSummaryDTO { 
                        Id = p.Id,

                        ProcedureId = p.ProcedureId,
                        ProcedureName = p.Procedure != null ? p.Procedure.Name : string.Empty,

                        Notes = p.Notes,
                        DurationInMinutes = p.DurationInMinutes,
                        AnesthesiaUsed = p.AnesthesiaUsed
                    }).ToList()


                }).ToListAsync();
        }

        public async Task<MedicalRecordResponseDTO?> GetMedicalRecordByIdAsync(int id)
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new MedicalRecordResponseDTO {
                    Id = m.Id,
                    VisitDate = m.VisitDate,
                    Symptoms = m.Symptoms,
                    Notes = m.Notes,

                    //flatten data
                    AnimalId = m.AnimalId,
                    AnimalCode = m.Animal != null ? m.Animal.Code : string.Empty,
                    AnimalName = m.Animal != null ? m.Animal.Name : string.Empty,
                    AnimalSpecies = m.Animal != null ? m.Animal.Species : m.Animal!.Species,

                    UserId = m.UserId,
                    VeterinarianName = m.User != null ? m.User.Name : string.Empty,

                    DiagnosisId = m.DiagnosisId,
                    DiagnosisCode = m.Diagnosis != null ? m.Diagnosis.Code : string.Empty,
                    DiagnosisName = m.Diagnosis != null ? m.Diagnosis.Name : string.Empty,

                    Prescriptions = m.Prescriptions.Select(p => new PrescriptionSummaryDTO
                    {
                        Id = p.Id,
                        MedicationId = p.MedicationId,
                        MedicationName = p.Medication != null ? p.Medication.Name : string.Empty,
                    }).ToList(),

                    ProcedureRecords = m.ProcedureRecords.Select(p => new ProcedureRecordSummaryDTO
                    {
                        Id = p.Id,

                        ProcedureId = p.ProcedureId,
                        ProcedureName = p.Procedure != null ? p.Procedure.Name : string.Empty,

                        Notes = p.Notes,
                        DurationInMinutes = p.DurationInMinutes,
                        AnesthesiaUsed = p.AnesthesiaUsed
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<MedicalRecordResponseDTO?> UpdateMedicalRecordAsync(MedicalRecordUpdateDTO updateMedicalRecord)
        {
            var existingMedicalRecord = await _context.MedicalRecords
                .IgnoreQueryFilters()
                .Include(m => m.Prescriptions)
                .Include(m => m.ProcedureRecords)
                .FirstOrDefaultAsync(m => m.Id == updateMedicalRecord.Id);

            if (existingMedicalRecord == null)
            {
                return null;
            }

            existingMedicalRecord.AnimalId = updateMedicalRecord.AnimalId;
            existingMedicalRecord.UserId = updateMedicalRecord.UserId;
            existingMedicalRecord.DiagnosisId = updateMedicalRecord.DiagnosisId;
            existingMedicalRecord.VisitDate = updateMedicalRecord.VisitDate;
            existingMedicalRecord.Symptoms = updateMedicalRecord.Symptoms;
            existingMedicalRecord.Notes = updateMedicalRecord.Notes;

            _context.Prescriptions.RemoveRange(existingMedicalRecord.Prescriptions);
            _context.ProcedureRecords.RemoveRange(existingMedicalRecord.ProcedureRecords);

            existingMedicalRecord.Prescriptions = new List<Prescription>();
            existingMedicalRecord.ProcedureRecords = new List<ProcedureRecord>();

            await _context.SaveChangesAsync();

            return await GetMedicalRecordByIdAsync(existingMedicalRecord.Id);


        }
    }
}
