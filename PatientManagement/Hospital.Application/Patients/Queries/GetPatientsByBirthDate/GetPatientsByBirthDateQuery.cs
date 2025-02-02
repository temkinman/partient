using FluentValidation;
using Hospital.Application.Dtos;
using Hospital.Application.Enums;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatientsByBirthDate;

public record GetPatientsByBirthDateQuery(DateTime BirthDate, DateParamsPrefix Prefix) : IRequest<GetPatientsByBirthDateResult>;

public record GetPatientsByBirthDateResult(IEnumerable<PatientDto> Patients);

public class GetPatientsByBirthDateQueryValidator : AbstractValidator<GetPatientsByBirthDateQuery>
{
    public GetPatientsByBirthDateQueryValidator()
    {
        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(1)).WithMessage("Invalid birth date");
    }
}