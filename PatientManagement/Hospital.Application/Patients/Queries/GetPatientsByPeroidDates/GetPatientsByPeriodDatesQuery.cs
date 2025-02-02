using System.Data;
using FluentValidation;
using Hospital.Application.Dtos;
using Hospital.Application.Enums;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatientsByPeroidDates;

public record GetPatientsByPeriodDatesQuery(
    DateParamsPrefix StartDatePrefix,
    DateParamsPrefix EndDatePrefix,
    DateTime StartDate,
    DateTime EndDate
    ) : IRequest<GetPatientsByPeriodDatesResult>;
    
public record GetPatientsByPeriodDatesResult(IEnumerable<PatientDto> Patients);

public class GetPatientsByPeriodDatesQueryValidator : AbstractValidator<GetPatientsByPeriodDatesQuery>
{
    public GetPatientsByPeriodDatesQueryValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Invalid birth date");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("Invalid birth date");
        RuleFor(x => x.StartDatePrefix).NotEqual(DateParamsPrefix.ge) .WithMessage("Invalid startDate prefix");
        RuleFor(x => x.EndDatePrefix).NotEqual(DateParamsPrefix.le) .WithMessage("Invalid endDate prefix");
    }
}

