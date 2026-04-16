using System;
using System.Collections.Generic;

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
            Console.WriteLine("Универсальный калькулятор запущен!");
            Console.WriteLine("Введите пример через пробел (например: 10 + 5) или 'exit' для выхода:");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit") break;

                try
                {
                   
                    string[] parts = input.Split(' ');

                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Ошибка! Формат: Число Знак Число");
                        continue;
                    }

                    
                    double n1 = Convert.ToDouble(parts[0]);
                    string op = parts[1];
                    double n2 = Convert.ToDouble(parts[2]);

                   
                    Execute(n1, n2, op);
                }
                catch
                {
                    Console.WriteLine("Ошибка: Введите корректные числа через пробел.");
                }
            }
        }

        
        static void Execute(double val1, double val2, string op)
        {
            double a = Convert.ToDouble(val1);
            double b = Convert.ToDouble(val2);

            var key = (typeof(double), op);

            if (calc.TryGetValue(key, out var action))
            {
                double result = action(a, b);
                Console.WriteLine($"Результат: {a} {op} {b} = {result}");
            }
            else
            {
                Console.WriteLine($"Операция {op} не найдена!");
            }
        }
    }
}
