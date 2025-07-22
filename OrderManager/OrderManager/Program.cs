using System;

class Program
{
    static void Main()
    {
        RunOrder();
    }

    static void RunOrder()
    {
        Console.WriteLine( "O Z O N" );

        string name = GetUserName();
        string product = GetProductName();
        int count = GetProductCount();
        string address = GetDeliveryAddress();

        ConfirmOrder( name, product, count, address );
    }

    static string GetUserName()
    {
        string name = GetInput( "Введите ваше имя: " );
        Console.WriteLine( $"Привет, {name}!" );
        return name;
    }

    static string GetProductName()
    {
        return GetInput( "Введите товар: " );
    }

    static int GetProductCount()
    {
        int count;
        while ( true )
        {
            Console.Write( "Введите количество: " );
            if ( int.TryParse( Console.ReadLine(), out count ) && count > 0 )
                return count;

            Console.WriteLine( "Ошибка: введите число больше нуля!" );
        }
    }

    static string GetDeliveryAddress()
    {
        return GetInput( "Введите адрес доставки: " );
    }

    static void ConfirmOrder( string name, string product, int count, string address )
    {
        Console.WriteLine( $"{name}, вы заказали {count} {product} на адрес {address}. Все верно? (да/нет)" );

        if ( Console.ReadLine().ToLower() == "да" )
        {
            DateTime today = DateTime.Now;
            Console.WriteLine( $"{name}, ваш заказ оформлен! Доставка по адресу {address} ожидается к {today.Day + 3} июля." );
        }
        else
        {
            Console.WriteLine( "Начнем заново...\n" );
            RunOrder();
        }
    }

    static string GetInput( string message )
    {
        string input;
        do
        {
            Console.Write( message );
            input = Console.ReadLine()?.Trim();

            if ( string.IsNullOrEmpty( input ) )
                Console.WriteLine( "Ошибка: поле не может быть пустым!" );

        } while ( string.IsNullOrEmpty( input ) );

        return input;
    }
}