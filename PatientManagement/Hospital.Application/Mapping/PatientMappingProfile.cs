using AutoMapper;
using Hospital.Application.Dtos;
using Hospital.Application.Patients.Commands.CreatePatient;
using Hospital.Application.Patients.Commands.UpdatePatient;
using Hospital.Domain.Entities;

namespace Hospital.Application.Mapping;

public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<CreatePatientCommand, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.Name.Use, src.Name.Given.First(), src.Name.Given.Last(), src.Name.Family)));

        CreateMap<UpdatePatientCommand, Patient>();

        CreateMap<NameDto, Name>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Given[0]))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Given[1]));

        CreateMap<Name, NameDto>()
            .ForMember(dest => dest.Given, opt => opt.MapFrom(src => new List<string> { src.Given[0], src.Given[1] }));
    }
}
