using AutoMapper;
using Hospital.Application.Dtos;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatientsByPeroidDates;

public class GetPatientsByPeriodDatesHandler : IRequestHandler<GetPatientsByPeriodDatesQuery, GetPatientsByPeriodDatesResult>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientsByPeriodDatesHandler(
        IMapper mapper,
        IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
        
    }
    public async Task<GetPatientsByPeriodDatesResult> Handle(GetPatientsByPeriodDatesQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Patient> patients = await _patientRepository.GetPatientsByPeriodBirthDatesAsync(query.StartDate, query.EndDate, cancellationToken);
        IEnumerable<PatientDto> patientDtos = _mapper.Map<IEnumerable<PatientDto>>(patients);
        
        return _mapper.Map<GetPatientsByPeriodDatesResult>(patientDtos);
    }
}