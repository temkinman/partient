using AutoMapper;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Patients.Commands.DeletePatient;

public class DeleteAllPatientsCommandHandler : IRequestHandler<DeleteAllPatientsCommand, DeleteAllPatientsCommandResult>
{
    private readonly IPatientRepository _patientRepository;
    private readonly INameRepository _nameRepository;
    private readonly IMapper _mapper;

    public DeleteAllPatientsCommandHandler(
        IMapper mapper,
        IPatientRepository patientRepository,
        INameRepository nameRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
        _nameRepository = nameRepository;
    }
    
    public async Task<DeleteAllPatientsCommandResult> Handle(DeleteAllPatientsCommand command, CancellationToken cancellationToken)
    {
        bool patientResult = await _patientRepository.RemoveAllItemsAsync(cancellationToken);
        bool nameResult = await _nameRepository.RemoveAllItemsAsync(cancellationToken);
        
        return new DeleteAllPatientsCommandResult(patientResult && nameResult);
    }
}