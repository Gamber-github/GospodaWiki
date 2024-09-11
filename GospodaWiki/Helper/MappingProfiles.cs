using AutoMapper;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Event;
using GospodaWiki.Dto.Items;
using GospodaWiki.Dto.Location;
using GospodaWiki.Dto.Player;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Models;

namespace GospodaWiki.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Character, CharacterDetailsDto>();
            CreateMap<CharacterDetailsDto, Character>();

            CreateMap<GetCharactersDto, Character>();
            CreateMap<Character, GetCharactersDto>();
            CreateMap<PostCharacterDto, Character>();

            CreateMap<PostCharacterDto, Character>();

            CreateMap<RpgSystem, GetRpgSystemsDto>();
            CreateMap<GetRpgSystemsDto, RpgSystem>();

            CreateMap<PostRpgSystemDto, RpgSystem>();
            CreateMap<GetRpgSystemDetailsDto, RpgSystem>();
            CreateMap<PutRpgSystemDto, RpgSystem>();

            CreateMap<EventDetailsDto, Event>();
            CreateMap<GetEventsDto, Event>();
            CreateMap<PostEventDto, Event>();
            CreateMap<PutEventDto, Event>();

            CreateMap<Player, GetPlayersDto>();
            CreateMap<GetPlayersDto, Player>();

            CreateMap<Location, LocationDetailsDto>();
            CreateMap<LocationDetailsDto, Location>();
            CreateMap<PostLocationDto, Location>();
            CreateMap<PutLocationDto, Location>();

            CreateMap<Series, GetSeriesDto>();
            CreateMap<GetSeriesDto, Series>();
            CreateMap<GetSeriesDetailsDto, Series>();
            CreateMap<PostSeriesDto, Series>();

            CreateMap<GetTagDetailsDto, Tag>();

            CreateMap<GetItemsDto, Item>();
            CreateMap<GetItemDetailsDto, Item>();
            CreateMap<PostItemDto, Item>();
            CreateMap<PutItemDto, Item>();
        }
    }
}
