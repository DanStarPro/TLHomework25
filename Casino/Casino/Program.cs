using Casino;

namespace CasinoProgram;

internal static class Program
{
    private const string GameName = @"
          ###    ###    ###   ###  #    #   ###
         #   #  #   #  #   #   #   ##   #  #   #      
         #      #   #  #       #   # #  #  #   #      
         #      #####   ###    #   #  # #  #   #
         #      #   #      #   #   #  # #  #   #      
         #   #  #   #  #   #   #   #   ##  #   #      
          ###   #   #   ###   ###  #    #   ###
        ";

    private static int balance;

    private static void Main()
    {
        PrintGameName( GameName );
        InitializeBalance();
        ShowMenu();
    }

    private static void PrintGameName( string gameArt )
    {
        Console.WriteLine( gameArt );
        Console.WriteLine();
    }

    private static void InitializeBalance()
    {
        Console.Write( "Здравствуйте, Вас приветствует платформа Casino.\nВведите, пожалуйста, Ваш баланс: " );

        while ( true )
        {
            string balanceStr = Console.ReadLine();
            if ( int.TryParse( balanceStr, out balance ) && balance >= 0 )
            {
                break;
            }

            Console.WriteLine( $"Введено неверное значение баланса: {balanceStr}. Попробуйте еще раз:" );
        }
    }

    private static void ShowMenu()
    {
        while ( true )
        {
            Console.WriteLine( "\nВыберите действие:" );
            Console.WriteLine( $"{( int )Operation.CheckBalance} - Проверить баланс" );
            Console.WriteLine( $"{( int )Operation.PlayGame} - Сделать ставку" );
            Console.WriteLine( $"{( int )Operation.Exit} - Выйти" );

            if ( int.TryParse( Console.ReadLine(), out int choice ) && Enum.IsDefined( typeof( Operation ), choice ) )
            {
                ProcessChoice( ( Operation )choice );

                if ( ( Operation )choice == Operation.Exit )
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine( "Ошибка: неверный выбор!" );
            }
        }
    }

    private static void ProcessChoice( Operation choice )
    {
        switch ( choice )
        {
            case Operation.CheckBalance:
                ShowBalance();
                break;
            case Operation.PlayGame:
                PlayGame();
                break;
            case Operation.Exit:
                Console.WriteLine( "Спасибо за игру! До свидания!" );
                break;
        }
    }

    private static void ShowBalance()
    {
        Console.WriteLine( $"\nВаш текущий баланс: {balance} мрот." );
    }

    private static void PlayGame()
    {
        if ( balance <= 0 )
        {
            ZeroBalance();
            return;
        }

        Console.Write( $"\nВведите ставку (макс {balance} мрот.): " );

        if ( !int.TryParse( Console.ReadLine(), out int bet ) || bet <= 0 || bet > balance )
        {
            Console.WriteLine( "Некорректная ставка!" );
            return;
        }

        const int multiplicator = 1;
        int randomNum = Random.Shared.Next( 1, 21 );
        Console.WriteLine( $"Выпало число: {randomNum}" );

        if ( randomNum >= 18 )
        {
            int win = bet * ( 1 + ( multiplicator * randomNum % 17 ) );
            balance += win;
            Console.WriteLine( $"Поздравляем! Вы выиграли {win} мрот.!" );
        }
        else
        {
            balance -= bet;
            Console.WriteLine( $"Вы проиграли {bet} мрот." );
        }

        if ( balance <= 0 )
        {
            ZeroBalance();
        }
    }
    private static void ZeroBalance()
    {
        Console.WriteLine( "У вас закончились деньги!" );
        Console.WriteLine( "1 - Пополнить баланс" );
        Console.WriteLine( "2 - Выйти из игры" );

        while ( true )
        {
            string input = Console.ReadLine();
            switch ( input )
            {
                case "1":
                    InitializeBalance();
                    return;
                case "2":
                    Console.WriteLine( "Спасибо за игру! До свидания!" );
                    Environment.Exit( 0 );
                    break;
                default:
                    Console.WriteLine( "Неверный выбор! Введите 1 или 2" );
                    break;
            }
        }
    }
}
