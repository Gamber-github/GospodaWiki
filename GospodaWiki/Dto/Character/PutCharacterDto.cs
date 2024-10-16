﻿using GospodaWiki.Models;

namespace GospodaWiki.Dto.Character
{
    public class PutCharacterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImagePath { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public int? SeriesId { get; set; }
        public int? RpgSystemId { get; set; }
        public ICollection<int>? TagsId { get; set; }
        public ICollection<int>? ItemsId { get; set; }
    }
}
