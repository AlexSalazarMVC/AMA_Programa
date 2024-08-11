using AutoMapper;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Dto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.FilterDto;
using FundacionAMA.Domain.DTO.BrigadeVolunteer.Request;
using FundacionAMA.Domain.Shared.Extensions.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundacionAMA.Domain.Mapping
{
    public class BrigadeVolunteerMapping:Profile
    {
        public BrigadeVolunteerMapping()
        {
            //CREAR
            CreateMap<BrigadeVolunteerRequest, BrigadeVoluntareer>().ReverseMap(); 

            ////GETALL 
            CreateMap<BrigadeVoluntareer, BrigadeVolunteerDto>()
                .ForMember(dest => dest.NombredeBrigada, opt => opt.MapFrom(src => src.Brigade.Name))
                .ForMember(dest => dest.descripcionBrigada, opt => opt.MapFrom(src => src.Brigade.Description))
                .ForMember(dest => dest.responsableBrigada, opt => opt.MapFrom(src => src.Brigade.Person.NameCompleted))
                .ForMember(dest => dest.NombreVoluntario, opt => opt.MapFrom(src => src.Volunteer.Person.NameCompleted))
                .ForMember(dest => dest.RolVoluntario, opt => opt.MapFrom(src => src.Volunteer.Person.Volunteer))
            .ReverseMap();

            //GETBYID
            CreateMap<BrigadeVoluntareer, BrigadeVolunteerGetByIdDto>()
                .ForMember(dest => dest.NombredeBrigada, opt => opt.MapFrom(src => src.Brigade.Name))
                .ForMember(dest => dest.NombreVoluntario, opt => opt.MapFrom(src => src.Volunteer.Person.NameCompleted))
            .ReverseMap();

            //UPDATE
            CreateMap<BrigadeVolunteerRequest, BrigadeVoluntareer>()
            .ForMember(dest => dest.BrigadeId, opt => opt.MapFrom(src => src.BrigadeId))
            .ForMember(dest => dest.VolunteerId, opt => opt.MapFrom(src => src.VolunteerId))
            .ReverseMap();

            




        }
    }
}
