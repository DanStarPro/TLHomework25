using System.Globalization;
namespace OrderManager;
internal static class Program
{
    private static void Main()
    {
        RunOrder();
    }

    private static void RunOrder()
    {
        Console.WriteLine( "O Z O N" );

        string name = GetUserName();
        string product = GetProductName();
        int count = GetProductCount();
        string address = GetDeliveryAddress();

        ConfirmOrder( name, product, count, address );
    }

    private static string GetUserName()
    {
        string name = GetInput( "Введите ваше имя: " );
        Console.WriteLine( $"Привет, {name}!" );
        return name;
    }

    private static string GetProductName()
    {
        return GetInput( "Введите товар: " );
    }

    private static int GetProductCount()
    {
        while ( true )
        {
            Console.Write( "Введите количество: " );
            if ( int.TryParse( Console.ReadLine(), out int count ) && count > 0 )
            {

                return count;
            }

            Console.WriteLine( "Ошибка: введите число больше нуля!" );
        }
    }

    private static string GetDeliveryAddress()
    {
        return GetInput( "Введите адрес доставки: " );
    }

    private static void ConfirmOrder( string name, string product, int count, string address )
    {
        while ( true ) 
        {
            Console.WriteLine( $"{name}, вы заказали {count} {product} на адрес {address}. Все верно? (да/нет)" );
            string answer = Console.ReadLine()?.Trim().ToLower();

            switch ( answer )
            {
                case "да":
                    DateTime deliveryDate = DateTime.Now.AddDays( 3 );
                    string formattedDate = deliveryDate.ToString( "d MMMM yyyy г.", CultureInfo.GetCultureInfo( "ru-RU" ) );
                    Console.WriteLine( $"{name}, ваш заказ оформлен! Доставка по адресу {address} ожидается к {formattedDate}" );
                    return;

                case "нет":
                    Console.WriteLine( "Начнем заново...\n" );
                    RunOrder();
                    return;

                case null:
                default:
                    Console.WriteLine( "Введено неправильное значение! Попробуйте еще раз." );
                    break;
            }
        }
    }

    private static string GetInput( string message )
    {
        while ( true )
        {
            Console.Write( message );
            string? input = Console.ReadLine()?.Trim();

            if ( !string.IsNullOrEmpty( input ) )
            {
                return input;
            }

            Console.WriteLine( "Ошибка: поле не может быть пустым!" );
        }
    }
}