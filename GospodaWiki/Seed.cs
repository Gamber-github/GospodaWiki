using GospodaWiki.Data;
using GospodaWiki.Models;
using System.Text.Json;


namespace GospodaWiki
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public void SeedDataContext()
        {
            if (!dataContext.RpgSystemCharacters.Any())
            {
                var rpgSystemCharacters = new List<RpgSystemCharacter>()
                {
                    new RpgSystemCharacter()
                    {
                        Character = new Character()
                        {
                            FirstName = "Geralt",
                            LastName = "of Rivia",
                            Age = 100,
                            Birthday = new DateTime(1234, 1, 1),
                            Biography = "A witcher",
                            City = "Rivia",
                            Country = "Kaedwen",
                            Tags = new List<Tag>()
                            {
                                new Tag() { Name = "Witcher" },
                                new Tag() { Name = "Monster Hunter" }
                            },
                            CharacterAbilities = new List<CharacterAbility>()
                            {
                                new CharacterAbility()
                                {
                                    Ability = new Ability()
                                    {
                                        Name = "Swordsmanship",
                                        Description = "Master swordsman"
                                    }
                                }
                            },
                            CharacterEquipments = new List<CharacterEquipment>()
                            {
                                new CharacterEquipment()
                                {
                                    Equipment = new Equipment()
                                    {
                                        Name = "Silver Sword",
                                        Description = "For monsters"
                                    }
                                }
                            }
                        },
                        RpgSystem = new RpgSystem()
                        {
                            Name = "The Witcher",
                            Description = "A dark fantasy RPG"
                        }
                    }
                };
                dataContext.RpgSystemCharacters.AddRange(rpgSystemCharacters);
                dataContext.SaveChanges();
            }
        }
    }
}
