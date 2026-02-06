namespace ConsoleAppC_
{
    using System;
    public class Program
    {
        public static void Main()
        {
            Proga1();
            Proga2();
        }
        private static void Proga1()
        {
            int year = InputNum("Введите год: ");
            if (IsLeapYear(year))
                Console.WriteLine("Год {0} високосный", year);
            else
                Console.WriteLine("Год {0} не високосный", year);
        }
        private static int InputNum(string message)
        {
            string input;
            int num;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (!(int.TryParse(input, out num)))
                    Console.WriteLine("Введены неверные данные");
            }
            while (!(int.TryParse(input, out num)));
            return num;
        }
        private static bool IsLeapYear(int year)
        {
            return (year > 0 & year % 4 == 0);
        }
        private static void Proga2()
        {
            char degree_class = C_or_F("Введите значение шкалы: ");
            int degree = InputNum("Введите температуру: ");
            Console.WriteLine("Результат: {0}", Degree_val(degree_class, degree));

        }
        private static char C_or_F(string message)
        {
            string decl;
            char degree_class;
            string decllow;
            do
            {
                Console.Write(message);
                decl = Console.ReadLine();
                decllow = decl.ToLower();
                if (!((char.TryParse(decl, out degree_class)) & (decllow == "c" | decllow == "f")))
                    Console.WriteLine("Введена неверная информация");
            }
            while (!((char.TryParse(decl, out degree_class)) & (decllow == "c" | decllow == "f")));
            return degree_class;
        }
        private static int Degree_val(char degree_class, int degree)
        {
            if (degree_class == 'c')
                return (degree * 9) / 5 + 32;
            else
                return (degree - 32) * 5 / 9;
        }

    }
}
