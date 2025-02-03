using AutoMapper;
using Hospital.Api.Dtos;
using Hospital.Application.Dtos;
using Hospital.Application.Patients.Commands.CreatePatient;
using Hospital.Application.Patients.Commands.UpdatePatient;
using Hospital.Application.Patients.Queries.GetPatientById;
using Hospital.Domain.Entities;

namespace Hospital.Api.Mapping;

public class PatientRequestProfile : Profile
{
    public PatientRequestProfile()
    {
        CreateMap<Patient, PatientDto>();
        CreateMap<UpdatePatientRequest, UpdatePatientCommand>();
        
        CreateMap<GetPatientByIdResult, PatientDto>()
            .ForMember(dest => dest.Id, opt=> opt.MapFrom(src => src.Patient.Id));

        CreateMap<CreatePatientRequest, CreatePatientCommand>();
    }
}