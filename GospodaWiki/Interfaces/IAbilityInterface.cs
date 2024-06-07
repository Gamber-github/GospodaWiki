using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IAbilityInterface
    {
        ICollection<Ability> GetAbilities();
        Ability GetAbility(int id);
        Ability GetAbility(string name);
        bool AbilityExists(int abilityId);
        bool CreateAbility(Ability ability);
        bool UpdateAbility(Ability ability);
        bool Save();
        bool DeleteAbility(Ability ability);
    }
}
