using AutoMapper;
using Models;
using ServicesQueries.Dto;
using Utils;

namespace Services.Mapper
{
    public class AutomapperToDto : Profile
    {
        public AutomapperToDto()
        {
            //-- Users -----------

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<DataCollection<User>, DataCollection<UserDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            //-- ProfileDAncer -----------
            CreateMap<ProfileDancer, ProfileDancerDto>().ReverseMap();
            CreateMap<DataCollection<ProfileDancer>, DataCollection<ProfileDancerDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            //-- DanceLevel -----------
            CreateMap<DanceLevel, DanceLevelDto>().ReverseMap();
            CreateMap<DataCollection<DanceLevel>, DataCollection<DanceLevelDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            //-- DanceRol -----------
            CreateMap<DanceRol, DanceRolDto>().ReverseMap();
            CreateMap<DataCollection<DanceRol>, DataCollection<DanceRolDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));


            //-- TypeEvenet_User -----------
            CreateMap<TypeEventUser, TypeEventUserDto>().ReverseMap();
            CreateMap<DataCollection<TypeEventUser>, DataCollection<TypeEventUserDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            //-- TypeEvenet -----------
            CreateMap<TypeEvent, TypeEventDto>().ReverseMap();
            CreateMap<DataCollection<TypeEvent>, DataCollection<TypeEventDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

        }

    }

}