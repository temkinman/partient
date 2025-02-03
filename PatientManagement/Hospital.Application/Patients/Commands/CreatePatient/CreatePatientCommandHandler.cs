using AutoMapper;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, CreatePatientCommandResult>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public CreatePatientCommandHandler(
        IMapper mapper,
        IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }
    
    public async Task<CreatePatientCommandResult> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        Patient patientInput = _mapper.Map<Patient>(command);

        Patient addedPatient = await _patientRepository.CreateAsync(patientInput, cancellationToken);

        return new CreatePatientCommandResult(addedPatient.Id);
    }
}