using System.Linq.Expressions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly PatientDbContext _context;
    
    public PatientRepository(PatientDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Patients
            .Include(x => x.Name)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Patient?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .Include(x => x.Name)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Patient> CreateAsync(Patient patient, CancellationToken cancellationToken)
    {
        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task<Patient> UpdateAsync(Patient patient, CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task<bool> DeleteAsync(Patient patient, CancellationToken cancellationToken)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<IEnumerable<Patient>> GetPatientsByBirthDateAsync(Expression<Func<Patient, bool>> conditionExpression, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AsNoTracking()
            .Include(x => x.Name)
            .Where(conditionExpression)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByPeriodBirthDatesAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AsNoTracking()
            .Include(x => x.Name)
            .Where(x => x.BirthDate >= startDate && x.BirthDate <= endDate)
            .ToListAsync(cancellationToken);
    }
}