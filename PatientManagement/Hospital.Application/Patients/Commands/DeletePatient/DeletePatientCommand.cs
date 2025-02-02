using FluentValidation;
using MediatR;

namespace Hospital.Application.Patients.Commands.DeletePatient;

public record DeletePatientCommand(Guid PatientId) : IRequest<DeletePatientCommandResult>;

public record DeletePatientCommandResult(bool IsSuccess);

public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
{
    public DeletePatientCommandValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty().WithMessage("PatientId cannot be empty");
    }
}
