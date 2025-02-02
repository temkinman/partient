using AutoMapper;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, DeletePatientCommandResult>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public DeletePatientCommandHandler(
        IMapper mapper,
        IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }
    
    public async Task<DeletePatientCommandResult> Handle(DeletePatientCommand command, CancellationToken cancellationToken)
    {
        Patient? existingPatient = await _patientRepository.GetItemByIdAsync(command.PatientId, cancellationToken);
        if (existingPatient == null)
        {
            throw new NotFoundException(nameof(Patient),$"Patient with this id '{command.PatientId}' wasn't found.");
        }
        
        await _patientRepository.DeleteAsync(existingPatient, cancellationToken);

        return new DeletePatientCommandResult(true);
    }
}