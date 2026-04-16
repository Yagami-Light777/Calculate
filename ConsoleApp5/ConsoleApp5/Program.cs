using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Programm
{
    public class Program
    {

        static Dictionary<(Type, string), Func<double, double, double>> calc = new Dictionary<(Type, string), Func<double, double, double>>
        {
            [(typeof(double), "+")] = (a, b) => a + b,
            [(typeof(double), "-")] = (a, b) => a - b,
            [(typeof(double), "*")] = (a, b) => a * b,
            [(typeof(double), "/")] = (a, b) => b != 0 ? a / b : 0,
            [(typeof(double), "%")] = (a, b) => a * (b / 100),
        };

        static void Main(string[] args)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("--- Универсальный калькулятор запущен ---");
            Console.WriteLine("Можно писать: 10+5, 10 / 0.5, 100%10");
            Console.WriteLine("Для выхода напишите 'exit'");

            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.ToLower() == "exit") break;

                try
                {

                    char[] operators = { '+', '-', '*', '/', '%' };
                    int opIndex = input.IndexOfAny(operators);

                    if (opIndex == -1)
                    {
                        Console.WriteLine("Ошибка: Знак операции не найден.");
                        continue;
                    }


                    string s1 = input.Substring(0, opIndex).Trim();
                    string op = input[opIndex].ToString();
                    string s2 = input.Substring(opIndex + 1).Trim();


                    double n1 = Convert.ToDouble(s1);
                    double n2 = Convert.ToDouble(s2);

                    Execute(n1, n2, op);
                }
                catch
                {
                    Console.WriteLine("Ошибка: Неверный формат числа или примера.");
                }
            }
        }

        static void Execute(double val1, double val2, string op)
        {

            var key = (typeof(double), op);

            if (calc.TryGetValue(key, out var action))
            {
                double result = action(val1, val2);
                Console.WriteLine($"Результат: {val1} {op} {val2} = {result}");
            }
            else
            {
                Console.WriteLine($"Операция '{op}' не поддерживается!");
            }
        }
    }
}
