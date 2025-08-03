using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Services
{
    public static class FighterFactory
    {
        public static IFighter CreateFighter()
        {
            Console.WriteLine( "\n=== Создание нового бойца ===" );

            string name = GetName();
            IRace race = SelectRace();
            IWeapon weapon = SelectWeapon();
            IArmor armor = SelectArmor();

            var fighter = new Knight( name, race )
            {
                Weapon = weapon,
                Armor = armor
            };

            Console.WriteLine( "\nБоец успешно создан!" );
            Console.WriteLine( fighter.GetInfo() );

            return fighter;
        }

        private static string GetName()
        {
            while ( true )
            {
                Console.Write( "\nВведите имя бойца: " );
                string name = Console.ReadLine()?.Trim();

                if ( !string.IsNullOrEmpty( name ) )
                {
                    return name;
                }

                Console.WriteLine( "Имя не может быть пустым!" );
            }
        }

        private static IRace SelectRace()
        {
            var races = new Dictionary<int, IRace>
            {
                { 1, new Human() },
                { 2, new Elf() },
                { 3, new Orc() }
            };

            Console.WriteLine( "Выберите расу:" );
            Console.WriteLine( "1. Человек (HP: 100, Урон: 1, Защита: 0)" );
            Console.WriteLine( "2. Эльф    (HP: 80,  Урон: 4, Защита: 1)" );
            Console.WriteLine( "3. Орк     (HP: 120, Урон: 2, Защита: 3)" );

            return SelectFromMenu( races, "расу" );
        }

        private static IWeapon SelectWeapon()
        {
            var weapons = new Dictionary<int, IWeapon>
            {
                { 1, new Firsts() },
                { 2, new Sword() },
                { 3, new Axe() },
                { 4, new Bow() }
            };

            Console.WriteLine( "\nВыберите оружие:" );
            Console.WriteLine( "1. Кулаки (Урон: 1)" );
            Console.WriteLine( "2. Меч (Урон: 15)" );
            Console.WriteLine( "3. Топор (Урон: 20)" );
            Console.WriteLine( "4. Лук (Урон: 12)" );

            return SelectFromMenu( weapons, "оружие" );
        }

        private static IArmor SelectArmor()
        {
            var armors = new Dictionary<int, IArmor>
            {
                { 1, new NoArmor() },
                { 2, new LeatherArmor() },
                { 3, new Chainmail() },
                { 4, new PlateArmor() }
            };

            Console.WriteLine( "\nВыберите броню:" );
            Console.WriteLine( "1. Без брони (Защита: 0)" );
            Console.WriteLine( "2. Кожаная броня (Защита: 5)" );
            Console.WriteLine( "3. Кольчуга (Защита: 10)" );
            Console.WriteLine( "4. Латная броня (Защита: 15)" );

            return SelectFromMenu( armors, "броню" );
        }

        private static T SelectFromMenu<T>( Dictionary<int, T> options, string optionName )
        {
            while ( true )
            {
                Console.Write( $"\nВаш выбор (1-{options.Count}): " );
                if ( int.TryParse( Console.ReadLine(), out int choice ) && options.TryGetValue( choice, out T selectedOption ) )
                {
                    return selectedOption;
                }

                Console.WriteLine( $"Неверный выбор! Введите число от 1 до {options.Count} для выбора {optionName}." );
            }
        }
    }
}

