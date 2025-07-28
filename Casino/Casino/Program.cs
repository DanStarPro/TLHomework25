using Casino;

const string GameName = @"
  ###    ###    ###   ###  #    #   ###
 #   #  #   #  #   #   #   ##   #  #   #      
 #      #   #  #       #   # #  #  #   #      
 #      #####   ###    #   #  # #  #   #
 #      #   #      #   #   #  # #  #   #      
 #   #  #   #  #   #   #   #   ##  #   #      
  ###   #   #   ###   ###  #    #   ###
";

PrintGameName(GameName);
Console.Write("Здравствуйте, Вас приветствует платформа Casino. \nВведите, пожалуйста, Ваш баланс: ");
string balanceStr = Console.ReadLine();
bool isBalancedParced = int.TryParse(balanceStr, out int balance);
if (!isBalancedParced)
{
    Console.WriteLine($"Введено неверное значение баланса: {balanceStr}");
    return;
}

ShowMenu(ref balance); 

static void PrintGameName(string GameName)
{
    Console.WriteLine(GameName);
    Console.WriteLine();
}

static void ShowMenu(ref int balance) 
{
    while (true)
    {
        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine($"{(int)Operation.CheckBalance} - Проверить баланс");
        Console.WriteLine($"{(int)Operation.PlayGame} - Сделать ставку");
        Console.WriteLine($"{(int)Operation.Exit} - Выйти");

        if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(Operation), choice))
        {
            switch ((Operation)choice)
            {
                case Operation.CheckBalance:
                    ShowBalance(balance);
                    break;
                case Operation.PlayGame:
                    PlayGame(ref balance); 
                    break;
                case Operation.Exit:
                    return;
            }
        }
        else
        {
            Console.WriteLine("Ошибка: неверный выбор!");
        }
    }
}

static void ShowBalance(int balance)
{
    Console.WriteLine($"\nВаш текущий баланс: {balance} мрот.");
}

static void PlayGame(ref int balance) 
{
    Console.Write($"\nВведите ставку (макс {balance} мрот.): ");
    string betStr = Console.ReadLine();
    bool isBetParced = int.TryParse(betStr, out int bet);
    if (!isBetParced || bet <= 0 || bet > balance)
    {
        Console.WriteLine("Некорректная ставка!");
        return;
    }

    const int multiplicator = 1;
    Random rnd = new Random();
    int random_num = rnd.Next(1, 21);
    Console.WriteLine($"Выпало число: {random_num}");

    if (random_num >= 18)
    {
        int win = bet * (1 + (multiplicator * random_num % 17));
        balance += win;
        Console.WriteLine($"Поздравляем! Вы выиграли {win} мрот.!");
    }
    else
    {
        balance -= bet;
        Console.WriteLine($"Вы проиграли {bet} мрот.");
    }
}