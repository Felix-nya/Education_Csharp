using System;
using System.Text;

namespace Test
{
    class Program
    {
        internal static void TestEmp()
        {
            Console.WriteLine($"Имя компании: {Employee.CompanyName}, дата основания: {Employee.CompanyFoundedDate}");
            Employee[] company = { new Employee(3, "thr", 1000), new Manager(1, "fir", 10000, 50), new Developer(2, "sec", 5000, 10) };
            foreach (Employee emp in company)
            {
                Console.WriteLine($"{emp.GetEmployeeInfo()}, зарплата: {emp.CalculateSalary()}");
            }
        }
        internal static void TestSmartNum()
        {
            int num0 = 5;
            double num1 = 5.1;
            SmartNumber num2 = num0;
            SmartNumber num3 = num1;
            Console.WriteLine($"int: {num0}, double: {num1}, int to smartnumber: {num2}, double to smartnumber: {num3}");
            Console.WriteLine($"+: {num2 + num3}");
            Console.WriteLine($"-: {num2 - num3}");
            Console.WriteLine($"*: {num2 * num3}");
            Console.WriteLine($"/: {num2 / num3}");
            int num4 = (int)num2;
            double num5 = (double)num3;
            Console.WriteLine($"int to smartnumber: {num4}, double to smartnumber: {num5}");
            num2 = (SmartNumber)"5,5";
            string str1 = num3.ToString();
            Console.WriteLine($"string to smartNumber: {num2}, ToString: {str1}");
            string str2 = num3;
            Console.WriteLine($"smartnumber to string: {str2}");
            Console.WriteLine($"num2 == num3: {num2 == num3}");
            Console.WriteLine($"Equals: {num2.Equals(num3)}");
        }
        internal static void TestMathExt()
        {
            int[] numbers = { 2, 4, 4, 4, 5, 5, 7, 9 };
            Console.WriteLine($"Стандартное отклонение генеральная совокупность: {numbers.StandartDeviation()}");
            Console.WriteLine($"Стандартное отклонение выборочная дисперсия: {numbers.StandartDeviation(useSampleVariance: true)}");

            double radius = 5.0;
            if (MathExtensions.TryCalculateCircle(in radius, out double square, out double length))
            {
                Console.WriteLine($"Радиус: {radius}, площадь: {square:F2}, длина окружности: {length:F2}");
            }

            int x = 10, y = 20;
            Console.WriteLine($"До обмена: x = {x}, y = {y}");
            MathExtensions.SwapValues(ref x, ref y);
            Console.WriteLine($"После обмена: x = {x}, y = {y}");

            string formDefault = MathExtensions.FormatNumbers(numbers: [1.2345, 6.789, 10]);
            string formCustom = MathExtensions.FormatNumbers("F0", 1.2345, 6.789, 10);
            Console.WriteLine($"Форматирование F2: {formDefault}");
            Console.WriteLine($"Форматирование F0: {formCustom}");

            decimal yearly = MathExtensions.CalculateInvestment(principal: 100, rate: 10, years: 3, compoundMonthly: false);
            decimal monthly = MathExtensions.CalculateInvestment(principal: 100, rate: 10, years: 3);
            Console.WriteLine($"Инвестиция false: {yearly:F2}");
            Console.WriteLine($"Инвестиция true: {monthly:F2}");
        }
        internal static void TestMans()
        {
            Man[] mas = { new Teenager("Петя","Лицей 4",15), new Worker("Петрович", "ВТБ", 25),
                new Worker("Геннадий","Дом",17), new Teenager("Максим","Школа 7",14) };
            foreach (var person in mas) person.Describe();
            foreach (var person in mas)
            {
                Random rand = new();
                try
                {
                    person.SetAge(rand.Next(10, 80));
                    person.Describe();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        internal static void TestLongNumber()
        {
            LongNumber num1 = new(1111111);
            LongNumber num2 = new(num1);
            Console.WriteLine($"num1: {num1}, num2: {num2}");
            Console.WriteLine($"+: {num1 + num2}");
            Console.WriteLine($"-: {num1 - num2}");
            Console.WriteLine($"*: {num1 * num2}");
            Console.WriteLine($"/: {num1 / num2}");
            Console.WriteLine($"num1 == num2: {num1 == num2}");
            Console.WriteLine($"num1 != num2: {num1 != num2}");
            Console.WriteLine($"Equals: {num1.Equals(num2)}");
            int num3 = (int)num1;
            bool num4 = (bool)num2;
            LongNumber num5 = (LongNumber)num3;
            LongNumber num6 = (LongNumber)num4;
            Console.WriteLine($"LongNumber to int: {num3}, LongNumber to Bool: {num4}");
            Console.WriteLine($"int to LongNumber: {num5}, Bool to LongNumber: {num6}");
            string str1 = "123456789";
            StringBuilder sb1 = new StringBuilder("123987456");
            if (LongNumber.TryParse(str1, out num1)) Console.WriteLine($"TryParse str1: {str1} to num1: {num1}");
            LongNumber num7 = str1.ToLongNumber();
            LongNumber num8 = sb1.ToLongNumber();
            Console.WriteLine($"Extension: string: {str1} to longNumber: {num7}, stringBuilder: {sb1} to LongNumber: {num8}");
        }
        internal static void TestNode()
        {
            Node d = new Node("d");
            Node c = new Node("c"); c.Children.Add(d);
            Node f = new Node("f");
            Node e = new Node("e"); e.Children.Add(f);
            Node h = new Node("h"), iN = new Node("i");
            Node g = new Node("g"); g.Children.Add(h, iN);
            Node b = new Node("b"); b.Children.Add(c, e, g);
            Node l = new Node("l");
            Node k = new Node("k"); k.Children.Add(l);
            Node m = new Node("m");
            Node j = new Node("j"); j.Children.Add(k, m);
            Node a = new Node("a"); a.Children.Add(b, j);

            Console.WriteLine(a);

            a.Children.AddDefault();
            Console.WriteLine($"Узлов у 'a': {a.Children.Count}, последний Text = \"{a.Children[a.Children.Count - 1].Text}\"");

            Node outNode = new Node("было");
            b.Children.TryGet(99, out outNode);
            Console.WriteLine("out при неверном индексе: " + (outNode == null ? "null" : outNode.Text));

            Node refNode = new Node("было");
            b.Children.TryGetRef(99, ref refNode);
            Console.WriteLine("ref при неверном индексе: " + refNode.Text);

            Console.WriteLine(a);
        }
        static void Main(string[] args)
        {
            //TestEmp();
            //TestSmartNum();
            //TestMathExt();
            //TestMans();
            //TestLongNumber();
            TestNode();
            Console.ReadKey();
        }
    }
}