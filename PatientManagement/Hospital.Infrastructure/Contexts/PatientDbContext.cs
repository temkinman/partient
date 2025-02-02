using System.Reflection;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Contexts;

public class PatientDbContext : DbContext, IPatientDbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Name> PatientNames { get; set; }
    
    public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
        base.OnModelCreating(modelBuilder);
    }
}