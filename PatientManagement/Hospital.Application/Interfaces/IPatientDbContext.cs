using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Application.Interfaces;

public interface IPatientDbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Name> PatientNames { get; set; }
    Task<int> SaveChangesAsync(CancellationToken token = default);
}