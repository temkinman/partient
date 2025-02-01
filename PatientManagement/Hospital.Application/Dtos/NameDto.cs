namespace Hospital.Application.Dtos;

public record NameDto(
    Guid Id,
    string Use,
    string Family,
    List<string> Given
);