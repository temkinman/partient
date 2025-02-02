using AutoMapper;
using Hospital.Application.Dtos;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Patients.Commands.UpdatePatient;

public class UpdatePatientHandler : IRequestHandler< UpdatePatientCommand, UpdatePatientCommandResult>
{
    private readonly IPatientRepository _patientRepository;
    private readonly INameRepository _nameRepository;
    private readonly IMapper _mapper;

    public UpdatePatientHandler(
        IMapper mapper,
        IPatientRepository patientRepository,
        INameRepository nameRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
        _nameRepository = nameRepository;
    }
    
    public async Task<UpdatePatientCommandResult> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        Patient patientInput = _mapper.Map<Patient>(command);
        
        Patient? existingPatient = await _patientRepository.GetItemByIdAsync(patientInput.Id, cancellationToken);
        if (existingPatient == null)
        {
            throw new NotFoundException(nameof(Patient),$"Patient with this id '{patientInput.Id}' wasn't found.");
        }

        if (existingPatient.Name == null || existingPatient.Name.Id != patientInput.NameId)
        {
            throw new ConflictException($"Existing name id {patientInput.NameId} is not equal the nameId {existingPatient.NameId} related to this patient.");
        }
        
        Name? existingName = await _nameRepository.GetItemByIdAsync(existingPatient.NameId, cancellationToken);
        if (existingName == null)
        {
            throw new NotFoundException(nameof(Patient),$"Name with this id '{patientInput.Id}' wasn't found.");
        }
        
        existingPatient.Gender = patientInput.Gender;
        existingPatient.BirthDate = patientInput.BirthDate;
        existingPatient.Active = patientInput.Active;
        
        existingName.FirstName = patientInput.Name.FirstName;
        existingName.LastName = patientInput.Name.LastName;
        existingName.Family = patientInput.Name.Family;
        existingName.Use = patientInput.Name.Use;
        
        Patient updated = await _patientRepository.UpdateAsync(existingPatient, cancellationToken);
        PatientDto patientDto = _mapper.Map<PatientDto>(updated);
        
        return new UpdatePatientCommandResult(patientDto);
    }
}