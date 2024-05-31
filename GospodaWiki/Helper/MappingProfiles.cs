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
        }
    }
}
