using System.Text;

namespace CoffeeMachine.ConsoleApp;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var machine = new CoffeeMachine();
        machine.LoadWater(1000);
        machine.LoadMilk(500);
        machine.LoadBeans(200);

        while (true)
        {
            PrintMenu(machine);
            Console.Write("Ваш выбор: ");
            var choice = Console.ReadLine();

            if (choice == "0")
                break;

            try
            {
                if (!HandleChoice(machine, choice))
                    Console.WriteLine("Неизвестный пункт меню.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Некорректное значение: {ex.Message}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("До свидания!");
    }

    private static void PrintMenu(CoffeeMachine machine)
    {
        Console.WriteLine("=== Кофемашина ===");
        Console.WriteLine(
            $"Вода: {machine.GetWaterLevel()} | Молоко: {machine.GetMilkLevel()} | Зёрна: {machine.GetBeansLevel()}");
        Console.WriteLine("1. Эспрессо");
        Console.WriteLine("2. Фильтрованный");
        Console.WriteLine("3. Капучино");
        Console.WriteLine("4. Добавить воды");
        Console.WriteLine("5. Добавить молока");
        Console.WriteLine("6. Добавить зёрен");
        Console.WriteLine("0. Выход");
    }

    private static bool HandleChoice(CoffeeMachine machine, string? choice)
    {
        switch (choice)
        {
            case "1": Brew(machine, RecipeName.Espresso); return true;
            case "2": Brew(machine, RecipeName.Filter); return true;
            case "3": Brew(machine, RecipeName.Cappuccino); return true;
            case "4":
                machine.LoadWater(ReadAmount("Сколько воды добавить: "));
                Console.WriteLine("Вода добавлена.");
                return true;
            case "5":
                machine.LoadMilk(ReadAmount("Сколько молока добавить: "));
                Console.WriteLine("Молоко добавлено.");
                return true;
            case "6":
                machine.LoadBeans(ReadAmount("Сколько зёрен добавить: "));
                Console.WriteLine("Зёрна добавлены.");
                return true;
            default:
                return false;
        }
    }

    private static void Brew(CoffeeMachine machine, RecipeName recipe)
    {
        var coffee = machine.Brew(recipe);
        Console.WriteLine($"Готово! Ваш напиток: {GetTitle(coffee.Recipe)}.");
    }

    private static int ReadAmount(string prompt)
    {
        Console.Write(prompt);
        if (!int.TryParse(Console.ReadLine(), out var amount))
            throw new ArgumentException("Нужно ввести целое число.");

        return amount;
    }

    private static string GetTitle(RecipeName recipe) => recipe switch
    {
        RecipeName.Espresso => "эспрессо",
        RecipeName.Filter => "фильтрованный кофе",
        RecipeName.Cappuccino => "капучино",
        _ => recipe.ToString()
    };
}
