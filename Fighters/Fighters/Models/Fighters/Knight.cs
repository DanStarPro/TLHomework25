using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public class Knight : IFighter
    {
        private readonly IRace _race;
        public IWeapon Weapon { get; set; }
        public IArmor Armor { get; set; }

        public string Name { get; }
        public string ClassName => "Рыцарь";
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }

        public Knight( string name, IRace race )
        {
            Name = name ?? throw new ArgumentNullException( nameof( name ) );
            _race = race ?? throw new ArgumentNullException( nameof( race ) );
            Weapon = new Firsts();
            Armor = new NoArmor();
            MaxHealth = _race.Health + 50;
            CurrentHealth = MaxHealth;
        }

        public int CalculateDamage() => Weapon.Damage + _race.Damage + 5;
        public int CalculateArmor() => Armor.Armor + _race.Armor + 3;

        public void SetArmor( IArmor armor )
        {
            Armor = armor ?? throw new ArgumentNullException( nameof( armor ) );
        }

        public void SetWeapon( IWeapon weapon )
        {
            Weapon = weapon ?? throw new ArgumentNullException( nameof( weapon ) );
        }

        public void Attack( IFighter opponent )
        {
            if ( opponent == null )
            {
                throw new ArgumentNullException( nameof( opponent ) );
            }

            int damage = CalculateDamage();
            opponent.TakeDamage( damage );
        }

        public bool IsAlive() => CurrentHealth > 0;

        public void TakeDamage( int damage )
        {
            int actualDamage = Math.Max( damage - CalculateArmor(), 0 );
            CurrentHealth = Math.Max( CurrentHealth - actualDamage, 0 );
        }

        public string GetInfo()
        {
            return $"{Name} ({ClassName})\n" +
                   $"HP: {CurrentHealth}/{MaxHealth}\n" +
                   $"Урон: {CalculateDamage()}\n" +
                   $"Защита: {CalculateArmor()}\n" +
                   $"Оружие: {Weapon.Name}\n" +
                   $"Броня: {Armor.Name}";
        }
    }
}
