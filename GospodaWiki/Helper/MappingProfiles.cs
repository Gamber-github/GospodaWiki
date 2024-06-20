﻿using AutoMapper;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Event;
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

            CreateMap<CharactersDto, Character>();
            CreateMap<Character, CharactersDto>();
            CreateMap<PostCharacterDto, Character>();
            CreateMap<PutCharacterDto, Character>();

            CreateMap<PostCharacterDto, Character>();

            CreateMap<RpgSystem, GetRpgSystemsDto>();
            CreateMap<GetRpgSystemsDto, RpgSystem>();

            CreateMap<PostRpgSystemDto, RpgSystem>();
            CreateMap<GetRpgSystemDetailsDto, RpgSystem>();
            CreateMap<PutRpgSystemDto, RpgSystem>();

            CreateMap<EventDetailsDto, Event>();
            CreateMap<EventsDto, Event>();
            CreateMap<PostEventDto, Event>();
            CreateMap<PutEventDto, Event>();

            CreateMap<Player, GetPlayerDetailsDto>();
            CreateMap<GetPlayerDetailsDto, Player>();
            CreateMap<PostCharacterDto, Player>();
            CreateMap<PutPlayerDto, Player>();

            CreateMap<Location, LocationDetailsDto>();
            CreateMap<LocationDetailsDto, Location>();
            CreateMap<PostLocationDto, Location>();
            CreateMap<PutLocationDto, Location>();

            CreateMap<Series, GetSeriesDto>();
            CreateMap<GetSeriesDto, Series>();
            CreateMap<GetSeriesDetailsDto, Series>();
            CreateMap<PostSeriesDto, Series>();

            CreateMap<TagDetailsDto, Tag>();

        }
    }
}
