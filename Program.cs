using System;

namespace Test
{
    class Program
    {
        internal static void Test1()
        {
            String a = Console.ReadLine();
            String b = Console.ReadLine();
            String c = Console.ReadLine();
            try
            {
                var A = Double.Parse(a);
                var B = Double.Parse(b);
                var C = Double.Parse(c);
                if (A < 0 || B < 0 || C < 0)
                {
                    Console.WriteLine("Введено отрицательное число");
                }
                else if (A * A + B * B < C * C || A * A + C * C < B * B || B * B + C * C < A * A)
                {
                    Console.WriteLine("Введен не треугольник");
                }
                else
                {
                    var p = (A + B + C) / 2;
                    Console.WriteLine(Math.Sqrt(p * (p - A) * (p - B) * (p - C)).ToString("F2"));
                }
            }
            catch
            {
                Console.WriteLine("Введено не число");
            }
        }
        internal static void Test2()
        {
            try
            {
                var str = Console.ReadLine();
                int foo = int.Parse(str);
                if (foo < 100 || foo > 999)
                {
                    Console.WriteLine("Введено не 3х значное число!");
                }
                else if (Equals(str[2], '0') && Equals(str[1], '0')) Console.WriteLine($"{str[0]}");
                else if (Equals(str[2], '0')) Console.WriteLine($"{str[1]}{str[0]}");
                else Console.WriteLine($"{str[2]}{str[1]}{str[0]}");
            }
            catch
            {
                Console.WriteLine("Введено не число!");
            }
        }
        internal static void Test3()
        {
            char symbol = char.Parse(Console.ReadLine());
            int f = 400;
            try
            {
                //if (char.IsDigit(symbol)) f = 100;
                //else if (char.IsUpper(symbol)) f = 200;
                //else if (char.IsLower(symbol)) f = 300;
                f = symbol switch
                {
                    _ when char.IsDigit(symbol) => 100,
                    _ when char.IsUpper(symbol) => 200,
                    _ when char.IsLower(symbol) => 300
                };
                Console.WriteLine($"{symbol} : {f}");
            }
            catch
            {
                Console.WriteLine($"{symbol} : {f}");
            }

        }
        internal static void Test4()
        {
            string str = string.Empty;
            int r1 = 0;
            int r2 = 0;
            int r3 = 0;
            int flag = 1;
            do
            {
                str = Console.ReadLine();
                if (Equals(str, "0")) break;
                for (int i = 0; i < str.Length; i++)
                {
                    if (char.IsLetter(str[i]))
                    {
                        flag = 3;
                        break;
                    }
                    else if (Equals(str[i], '.') && flag != 2)
                    {
                        flag = 2;
                    }
                }
                switch (flag)
                {
                    case 1: r1++; break;
                    case 2: r2++; break;
                    case 3: r3++; break;
                }
                flag = 1;
            } while (!Equals(str, "0"));
            Console.WriteLine($"Целых чисел: {r1}, Вещественных: {r2}, Нечисловых: {r3}");
        }
        internal static void Test5()
        {
            float res1 = 0;
            double res2 = 0;
            decimal res3 = 0;
            for (int i = 1; i < 10000000; i++)
            {
                res1 += (float)(1f / (i * (i + 1) * (i + 2)));
                res2 += (double)(1d / (i * (i + 1) * (i + 2)));
                res3 += (decimal)(1d / (i * (i + 1) * (i + 2)));
            }
            Console.WriteLine($"Float: {res1}");
            Console.WriteLine($"Double: {res2}");
            Console.WriteLine($"Decimal: {res3}");
        }
        internal static void Test6()
        {
            string num = Console.ReadLine();
            int k = int.Parse(Console.ReadLine());
            if (k <= num.Length && k >= 0) Console.WriteLine(num.Substring(0, k));
            else Console.WriteLine("Введено неверное число разрядов");
        }
        static void Main(string[] args)
        {
            Test2();
        }
    }
}