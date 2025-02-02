using Hospital.Application.Dtos;
using Hospital.Domain.Enums;

namespace Hospital.Api.Dtos;

public record CreatePatientRequest(
    NameDto Name,
    Gender Gender,
    // DateTime BirthDate,
    bool Active)
{
    public DateTime BirthDate { get; set; }
}