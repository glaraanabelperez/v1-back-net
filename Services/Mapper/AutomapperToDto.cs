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

            CreateMap<Cycle, CycleDto>().ReverseMap();
            CreateMap<DataCollection<Cycle>, DataCollection<CycleDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<DataCollection<Event>, DataCollection<EventDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<DataCollection<Address>, DataCollection<AddressDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<DataCollection<City>, DataCollection<CityDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));

            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<DataCollection<Country>, DataCollection<CountryDto>>().ForMember(dest => dest.Items, sour => sour.MapFrom(s => s.Items));


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