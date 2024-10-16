﻿namespace GospodaWiki.Models
{
    public class RpgSystem
    {
        public int RpgSystemId { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public Image? Image { get; set; }
        public string? StoryName { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Character>? Characters { get; set; }
        public ICollection<Series>? Series { get; set; }
        public ICollection<Adventure>? Adventures { get; set; }
        public bool isPublished { get; set; } = false;
    }
}
