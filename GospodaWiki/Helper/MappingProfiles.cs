using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Models;

namespace GospodaWiki.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Character, CharacterDto>();
            CreateMap<CharacterDto, Character>();

            CreateMap<RpgSystem, RpgSystemDto>();
            CreateMap<RpgSystemDto, RpgSystem>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            CreateMap<Ability, AbilityDto>();
            CreateMap<AbilityDto, Ability>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
        }
    }
}
