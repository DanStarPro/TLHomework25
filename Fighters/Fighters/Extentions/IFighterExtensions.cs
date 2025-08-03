using Fighters.Models.Fighters;

namespace Fighters.Extensions
{
    public static class IFighterExtensions
    {
        public static string GetDetailedInfo( this IFighter fighter )
        {
            return $"[{fighter.Name}] | Класс: {fighter.ClassName} | " +
                   $"HP: {fighter.CurrentHealth}/{fighter.MaxHealth} | " +
                   $"Урон: {fighter.CalculateDamage()} | Защита: {fighter.CalculateArmor()}";
        }

        public static bool CanAttack( this IFighter fighter ) => fighter.IsAlive();
    }
}
