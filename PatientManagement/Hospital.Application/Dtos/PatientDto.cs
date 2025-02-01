using Hospital.Domain.Enums;

namespace Hospital.Application.Dtos;

public record PatientDto(
        Guid Id,
        NameDto Name,
        Gender Gender,
        DateTime BirthDate,
        bool Active
    );