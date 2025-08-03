using Fighters.GameEngine;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Services;

namespace Fighters
{
    public class LoadingScreen
    {
        public static void Main( string[] args )
        {
            IFightGameEngine gameEngine = new FightGameEngine();
            IRace human = new Human();
            IWeapon fists = new Firsts();
            IArmor noArmor = new NoArmor();

            Console.WriteLine( @"
    ##### ###  ###   #   # ##### #####  ####    ###
    #   #  #  #   #  #   #   #   #      #   #  #   #  
    #      #  #      #   #   #   #      #   #  #    
    ###    #  #      #####   #   ###    ####    ###
    #      #  #  ##  #   #   #   #      #  #       #
    #      #  #   #  #   #   #   #      #   #  #   #
    #     ###  ###   #   #   #   #####  #    #  ###
                " );
            Console.WriteLine( "    Добро пожаловать в игру Бойцы!" );

            while ( true )
            {
                Console.WriteLine( @"    Выберете нужную команду:
    1 - Добавить бойца
    2 - Начать битву
    3 - Посмотреть бойцов
    4 - Очистить список бойцов
    5 - Выйти" );
                Console.Write( "> " );

                string command = Console.ReadLine()?.ToLower();

                switch ( command )
                {
                    case "1":
                        try
                        {
                            IFighter fighter = FighterFactory.CreateFighter();
                            gameEngine.AddFighter( fighter );
                        }
                        catch ( Exception ex )
                        {
                            Console.WriteLine( $"Ошибка при создании бойца: {ex.Message}" );
                        }
                        break;

                    case "2":
                        try
                        {
                            gameEngine.StartBattle();
                            // После битвы спрашиваем, хочет ли игрок продолжить
                            Console.WriteLine( "\nХотите создать новых бойцов? (y/n)" );
                            if ( Console.ReadLine()?.ToLower() != "y" )
                            {
                                return;
                            }
                            gameEngine.ClearFighters(); // Очищаем список для новой игры
                        }
                        catch ( Exception ex )
                        {
                            Console.WriteLine( $"Ошибка: {ex.Message}" );
                        }
                        break;
                    case "3":
                        gameEngine.ShowFighters();
                        break;

                    case "4":
                        gameEngine.ClearFighters();
                        Console.WriteLine( "Список бойцов очищен!" );
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine( "Неизвестная команда" );
                        break;
                }
            }
        }
    }
}