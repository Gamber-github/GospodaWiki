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
            CreateMap<RpgSystem, RpgSystemDto>();
            CreateMap<RpgSystemDto, RpgSystem>();
            CreateMap<User, UserDto>();
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();
        }
    }
}
