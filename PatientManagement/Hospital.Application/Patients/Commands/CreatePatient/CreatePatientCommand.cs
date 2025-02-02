using FluentValidation;
using Hospital.Application.Dtos;
using Hospital.Domain.Enums;
using MediatR;

namespace Hospital.Application.Patients.Commands.CreatePatient;

public record CreatePatientCommand(
    NameDto Name,
    Gender Gender,
    DateTime BirthDate,
    bool Active) : IRequest<CreatePatientCommandResult>;

public record CreatePatientCommandResult(Guid PatientId);

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(p => p.BirthDate).NotNull().WithMessage("Birth Date is required.");
        RuleFor(p => string.IsNullOrWhiteSpace(p.Name.Family)).NotNull().WithMessage("Family is required.");
    }
}