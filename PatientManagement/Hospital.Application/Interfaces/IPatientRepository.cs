using Hospital.Domain.Entities;

namespace Hospital.Application.Interfaces;

public interface IPatientRepository : IBaseItemRepository<Patient>
{
    Task<IEnumerable<Patient>> GetPatientsByBirthDateAsync(DateTime birthDate, CancellationToken cancellationToken = default);
}
