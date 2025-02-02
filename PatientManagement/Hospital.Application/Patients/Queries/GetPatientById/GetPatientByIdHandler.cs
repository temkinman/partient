using AutoMapper;
using Hospital.Application.Dtos;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatientById;

public class GetPatientByIdHandler : IRequestHandler<GetPatientByIdQuery, GetPatientByIdResult>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientByIdHandler(
        IMapper mapper,
        IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }

    public async Task<GetPatientByIdResult> Handle(GetPatientByIdQuery query, CancellationToken cancellationToken)
    {
        Patient? patient = await _patientRepository.GetItemByIdAsync(query.PatientId, cancellationToken);

        if (patient == null) 
        {
            throw new NotFoundException(nameof(Patient), query.PatientId);
        }

        PatientDto patientDto = _mapper.Map<PatientDto>(patient);

        return new GetPatientByIdResult(patientDto);
    }
}
