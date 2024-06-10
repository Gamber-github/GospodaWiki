using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Event;
using GospodaWiki.Dto.Location;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Models;

namespace GospodaWiki.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Character, CharacterDetailsDto>();
            CreateMap<CharacterDetailsDto, Character>();

            CreateMap<CharactersDto, Character>();
            CreateMap<Character, CharactersDto>();
            CreateMap<PostCharacterDto, Character>();

            CreateMap<PostCharacterDto, Character>();

            CreateMap<RpgSystem, RpgSystemsDto>();
            CreateMap<RpgSystemsDto, RpgSystem>();

            CreateMap<PostRpgSystemDto, RpgSystem>();
            CreateMap<RpgSystemDetailsDto, RpgSystem>();
            CreateMap<PatchRpgSystemDto, RpgSystem>();

            CreateMap<EventDetailsDto, Event>();
            CreateMap<EventsDto, Event>();
            CreateMap<PostEventDto, Event>();
            CreateMap<PatchEventDto, Event>();

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();

            CreateMap<Location, LocationDetailsDto>();
            CreateMap<LocationDetailsDto, Location>();
            CreateMap<PostLocationDto, Location>();
            CreateMap<PatchLocationDto, Location>();

            CreateMap<Series, SeriesDto>();
            CreateMap<SeriesDto, Series>();
            CreateMap<SeriesDetailsDto, Series>();
            CreateMap<PostSeriesDto, Series>();
        }
    }
}
