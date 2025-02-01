using FluentValidation;
using Hospital.Application.Dtos;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatientById;

public record GetPatientByIdQuery(Guid PatientId) : IRequest<GetPatientByIdResult>;

public record GetPatientByIdResult(PatientDto Patient);

public class GetCategoryByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty().WithMessage("PatientId is required");
    }
}