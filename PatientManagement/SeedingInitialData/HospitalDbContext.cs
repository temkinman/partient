using Hospital.Domain.Entities;
using Hospital.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace SeedingInitialData;

public class HospitalDbContext : DbContext
{
    public DbSet<Name> PatientNames { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=patientDb;Port=5432;Database=PatientApiDb;Username=postgres;Password=1234");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NameConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
    }
}