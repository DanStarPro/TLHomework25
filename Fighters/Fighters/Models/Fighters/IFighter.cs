using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public interface IFighter
    {
        string Name { get; }
        string ClassName { get; }
        int CurrentHealth { get; }
        int MaxHealth { get; }

        int CalculateDamage();
        int CalculateArmor();
        void SetArmor( IArmor armor );
        void SetWeapon( IWeapon weapon );
        void Attack( IFighter opponent );
        bool IsAlive();
        void TakeDamage( int damage );
        string GetInfo();
    }
}
