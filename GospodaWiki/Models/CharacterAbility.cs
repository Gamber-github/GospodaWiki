﻿namespace GospodaWiki.Models
{
    public class CharacterAbility
    {
        public int CharacterId { get; set; }
        public int AbilityId { get; set; }
        public Character Character { get; set; }
        public Ability Ability { get; set; }
    }
}
