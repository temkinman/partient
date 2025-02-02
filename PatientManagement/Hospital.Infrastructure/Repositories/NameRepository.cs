using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class NameRepository : INameRepository
{
    private readonly PatientDbContext _context;
    
    public NameRepository(PatientDbContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Name>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Name?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.PatientNames.FirstOrDefaultAsync<Name>(n => n.Id == id, cancellationToken);
    }

    public Task<Name> CreateAsync(Name item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Name> UpdateAsync(Name item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Name item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}