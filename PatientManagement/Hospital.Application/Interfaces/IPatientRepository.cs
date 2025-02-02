using System.Linq.Expressions;
using Hospital.Domain.Entities;

namespace Hospital.Application.Interfaces;

public interface IPatientRepository : IBaseItemRepository<Patient>
{
    Task<IEnumerable<Patient>> GetPatientsByBirthDateAsync(Expression<Func<Patient, bool>> conditionExpression, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByPeriodBirthDatesAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}
