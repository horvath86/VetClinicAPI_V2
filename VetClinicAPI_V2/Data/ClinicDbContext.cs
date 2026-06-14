using Microsoft.EntityFrameworkCore;
using VetClinicAPI_V2.Models;

namespace VetClinicAPI_V2.Data
{
    public class ClinicDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<ProcedureRecord> ProcedureRecords { get; set; }
        
        

        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //unique
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.LicenceNumber).IsUnique().HasFilter("[LicenceNumber] IS NOT NULL");
            modelBuilder.Entity<Diagnosis>().HasIndex(d => d.Code).IsUnique();
            modelBuilder.Entity<Medication>().HasIndex(m => m.Code).IsUnique();
            modelBuilder.Entity<Animal>().HasIndex(a => a.Code).IsUnique();
            modelBuilder.Entity<Procedure>().HasIndex(p => p.Code).IsUnique();

            //filter out soft deleted entities
            modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);
            modelBuilder.Entity<Animal>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<MedicalRecord>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Diagnosis>().HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<Prescription>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Medication>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<ProcedureRecord>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Procedure>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Appointment>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Owner>().HasQueryFilter(o => !o.IsDeleted);

            //deletition rules - for safety
            modelBuilder.Entity<MedicalRecord>().HasOne(m => m.Diagnosis).WithMany().HasForeignKey(m => m.DiagnosisId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Prescription>().HasOne(p => p.Medication).WithMany().HasForeignKey(p => p.MedicationId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProcedureRecord>().HasOne(p => p.Procedure).WithMany().HasForeignKey(p => p.ProcedureId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MedicalRecord>().HasOne(m => m.User).WithMany().HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>().HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
