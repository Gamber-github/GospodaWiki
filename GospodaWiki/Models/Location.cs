﻿namespace GospodaWiki.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
