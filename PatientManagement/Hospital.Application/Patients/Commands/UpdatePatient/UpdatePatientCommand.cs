using FluentValidation;
using Hospital.Application.Dtos;
using Hospital.Domain.Enums;
using MediatR;

namespace Hospital.Application.Patients.Commands.UpdatePatient;

public record UpdatePatientCommand(
    Guid Id,
    NameDto Name,
    Gender Gender,
    DateTime BirthDate,
    bool Active
    ) : IRequest<UpdatePatientCommandResult>;

public record UpdatePatientCommandResult(PatientDto Patient);

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(x => x.BirthDate).NotNull().WithMessage("BirthDate is required");
        RuleFor(x => string.IsNullOrWhiteSpace(x.Name.Family)).NotNull().WithMessage("Family is required.");
    }
}