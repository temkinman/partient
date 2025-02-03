using MediatR;

namespace Hospital.Application.Patients.Commands.DeletePatient;

public record DeleteAllPatientsCommand() : IRequest<DeleteAllPatientsCommandResult>;

public record DeleteAllPatientsCommandResult(bool IsSuccess);
