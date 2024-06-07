using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class AbilitiesRepository : IAbilityInterface
    {
        private readonly DataContext _context;

        public AbilitiesRepository(DataContext context)
        {
            _context = context;
        }
       
        public ICollection<Ability> GetAbilities()
        {
            return _context.Abilities.ToList();
        }

        public Ability GetAbility(int id)
        {
            return _context.Abilities.Where(p => p.AbilityId == id).FirstOrDefault();
        }

        public Ability GetAbility(string name)
        {
            return _context.Abilities.Where(p => p.Name.Trim().ToUpper() == name.Trim().ToUpper()).FirstOrDefault();
        }

        public bool AbilityExists(int abilityId)
        {
            return _context.Abilities.Any(a => a.AbilityId == abilityId);
        }

        public bool CreateAbility(Ability ability)
        {
            if(ability == null)
            { 
                throw new ArgumentNullException(nameof(ability));
            }

            _context.Abilities.Add(ability);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }
         public bool UpdateAbility(Ability ability)
        {
            if(ability == null)
            {
                throw new ArgumentNullException(nameof(ability));
            }

            _context.Abilities.Add(ability);
            return Save();
        }
        public bool DeleteAbility(Ability ability)
        {
            if(ability == null)
            {
                throw new ArgumentNullException(nameof(ability));
            }

            _context.Abilities.Remove(ability);
            return Save();
        }
    }
}
