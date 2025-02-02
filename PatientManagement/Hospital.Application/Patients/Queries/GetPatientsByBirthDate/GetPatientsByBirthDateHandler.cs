using AutoMapper;
using Hospital.Application.Dtos;
using Hospital.Application.Enums;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatientsByBirthDate;

public class GetPatientsByBirthDateHandler : IRequestHandler<GetPatientsByBirthDateQuery, GetPatientsByBirthDateResult>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientsByBirthDateHandler(
        IMapper mapper,
        IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }
    
    public async Task<GetPatientsByBirthDateResult> Handle(GetPatientsByBirthDateQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Patient> patients = new List<Patient>();
        switch (query.Prefix)
        {
            case DateParamsPrefix.eq:
            case DateParamsPrefix.ap:
                patients = await _patientRepository.GetPatientsByBirthDateAsync(p => p.BirthDate == query.BirthDate, cancellationToken);
                break;
            case DateParamsPrefix.ne:
                patients = await _patientRepository.GetPatientsByBirthDateAsync(p => p.BirthDate != query.BirthDate, cancellationToken);
                break;
            case DateParamsPrefix.lt:
            case DateParamsPrefix.eb:
                patients = await _patientRepository.GetPatientsByBirthDateAsync(p => p.BirthDate < query.BirthDate, cancellationToken);
                break;
            case DateParamsPrefix.gt:
                patients = await _patientRepository.GetPatientsByBirthDateAsync(p => p.BirthDate > query.BirthDate, cancellationToken);
                break;
            case DateParamsPrefix.ge:
                patients = await _patientRepository.GetPatientsByBirthDateAsync(p => p.BirthDate >= query.BirthDate, cancellationToken);
                break;
            case DateParamsPrefix.le:
                patients = await _patientRepository.GetPatientsByBirthDateAsync(p => p.BirthDate <= query.BirthDate, cancellationToken);
                break;
        }
        
        IEnumerable<PatientDto> patientsDto = _mapper.Map<IEnumerable<PatientDto>>(patients);
        
        return new GetPatientsByBirthDateResult(patientsDto);
    }
}