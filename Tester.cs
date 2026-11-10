using System.Reflection;
using Test.Classes;

namespace Test
{
    internal static class Tester
    {
        private static void Article(string article)
        {
            Console.WriteLine($"[----------* Задание: {article} *----------]");
            Console.WriteLine();
        }
        private static void Endlines()
        {
            for (int i = 0; i < 3; i++) Console.WriteLine();
        }

        internal static void TestDelegatesAndException()
        {
            TestCounter();
            TestOrderManager();
            TestCacheCalculator();
            TestAlertSystem();
            TestBankAccount();
            TestCalculator();
            TestShapes();
        }


        internal static void TestCounter()
        {
            Article("Счётчик");

            Counter counter = new();
            counter.CountReached += OnCountReached;
            for (int i = 0; i < 10; i++) counter.Increment();
            
            Endlines();
        }
        private static void OnCountReached(object? sender, CountReachedEventArgs e)
        {
            Console.WriteLine($"«Цель достигнута! Счетчик = {e.Count}»");
        }

        internal static void TestOrderManager()
        {
            Article("Менеджер заказов");

            OrderManager manager = new();
            manager.OrderPlaced += OnOrderPlaced;
            manager.OrderPlaced += (sender, e) => Console.WriteLine($"Заказ принят: [{e.DishName}] для стола [{e.TableNumber}]");
            manager.PlaceOrder("Том Ям", 1);
            manager.PlaceOrder("Рамен", 2);
            manager.OrderPlaced -= OnOrderPlaced;
            manager.PlaceOrder("Шашлык", 3);

            Endlines();
        }
        private static void OnOrderPlaced(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"Детали заказа: [{e.DishName}] | Стол: [{e.TableNumber}] | Время: [{e.OrderTime}]");
        }

        internal static void TestCacheCalculator()
        {
            Article("Кэш калькулятор");

            Console.WriteLine(CacheCalculator<int>.Calculate(5, e => e * e));
            Console.WriteLine(CacheCalculator<int>.Calculate(5, e => e * e));
            Console.WriteLine(CacheCalculator<string>.Calculate("Hello", e => e + e));

            Endlines();
        }

        internal static void TestAlertSystem()
        {
            Article("Система оповещений");

            AlertSystem alertSystem = new();
            alertSystem.AlertRaised += OnInfoAlertRaised;
            alertSystem.AlertRaised += OnWarningAlertRaised;
            alertSystem.AlertRaised += OnErrorAlertRaised;
            //alertSystem.AlertRaised += (EventHandler<AlertEventArgs>)Delegate.Combine(OnInfoAlertRaised, OnWarningAlertRaised, OnErrorAlertRaised);
            alertSystem.RaiseAlert(new(AlertLevel.Info, "Hi"));
            alertSystem.RaiseAlert(new(AlertLevel.Warning, "Hi"));
            alertSystem.RaiseAlert(new(AlertLevel.Error, "Hi"));
            alertSystem.AlertRaised -= OnInfoAlertRaised;
            alertSystem.RaiseAlert(new(AlertLevel.Info, "Hi"));

            Endlines();
        }
        private static void OnInfoAlertRaised(object? sender, AlertEventArgs e)
        {
            if (e.Level == AlertLevel.Info)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"INFO: {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void OnWarningAlertRaised(object? sender, AlertEventArgs e)
        {
            if (e.Level == AlertLevel.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"WARNING: {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void OnErrorAlertRaised(object? sender, AlertEventArgs e)
        {
            if (e.Level == AlertLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static void TestBankAccount()
        {
            Article("Счёт банка");

            BankAccount MyBank = new() { Balance = 100 };
            decimal amount = 0;
            while (amount != 101)
            {
                Console.Write("Для выхода введите 101. Введите сумму для снятия: ");
                try
                {
                    amount = decimal.Parse(Console.ReadLine());
                    MyBank.Withdraw(amount);
                    Console.WriteLine($"Операция успешна. Новый баланс: {MyBank.Balance}");
                }
                catch (InsufficientFundsException e)
                {
                    Console.WriteLine($"Ошибка: Недостаточно средств на счёте. {e.Message}");
                }
            }

            Endlines();
        }

        internal static void TestCalculator()
        {
            Article("Калькулятор");

            int value = 0;
            while (value != 101)
            {
                Console.Write("Для выхода введите 101. Введите число: ");
                try
                {
                    value = int.Parse(Console.ReadLine());
                    Console.WriteLine($"10 : {value} = {Calculator.Divide(10, value)}");
                    Console.WriteLine($"Квадратный корень из {value} = {Calculator.SquareRoot(value)}");
                }
                catch (DivisionByZeroException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (NegativeRadicandException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (MathOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Произошла непредвиденная ошибка!");
                }
            }

            Endlines();
        }

        internal static void TestShapes()
        {
            Article("Геометрические фигуры, исключения");

            Random random = new();
            for (int i = 0; i < 10; i++)
            {
                int shapeVariant = random.Next(1, 4);
                try
                {
                    try
                    {
                        switch (shapeVariant)
                        {
                            case 1:
                                _ = new Triangle(random.Next(1, 11), random.Next(1, 11), random.Next(1, 11));
                                break;
                            case 2:
                                _ = new Quadrangle(random.Next(1, 11), random.Next(1, 11), random.Next(1, 11), random.Next(1, 11));
                                break;
                            default:
                                _ = new Circle(random.Next(-5, 6));
                                break;
                        }
                    }
                    catch (GeometryException e) when (e.GetType() == typeof(TriangleException) || e.GetType() == typeof(QuadrangleException))
                    {
                        File.AppendAllLines("D:\\polygon_errors.log", [e.Message + $" {string.Join("; ", e.Parameters)}"]);
                        throw;
                    }
                }
                catch (GeometryException e)
                {
                    File.AppendAllLines("D:\\all_geometry_errors.log", [e.Message + $" {string.Join("; ", e.Parameters)}"]);
                }
            }

            Endlines();
        }

        internal static void TestFirstClass()
        {
            Article("Атрибуты, первый класс");

            //FirstClass fc = new();
            //var properties = fc.GetType().GetProperties();
            var properties = typeof(FirstClass).GetProperties();
            foreach (var property in properties)
            {
                var NameAtr = property.GetCustomAttribute<NameAttribute>();
                if (NameAtr != null) Console.WriteLine($"{property.Name} - {NameAtr.Descr}");
            }// !property.IsDefined(typeof(NameAttribute))

            Endlines();
        }

        internal static void TestInt32Validate()
        {
            Article("Валидатор целого числа");

            Int33 el = new(1, 5, 0, -25);
            try
            {
                Int32Validate.Validate(el);
            }
            catch (InvalidValueException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.CurrentValue);
                Console.WriteLine(e.Name);
                foreach (string str in e.ListOfExceptions) Console.WriteLine(str);
            }

            Endlines();
        }

        internal static void TestLinq12()
        {
            Article("LINQ 1-2 совмещённые");

            var arr = new int[] { 1, 3, 5, 8, 9, 1, 6, 4, 7, 8, 9, 2, 5, 2, 7, 8, 0 };
            var arrGroup = arr
                .GroupBy(e => e % 2 == 0 ? "Чётные" : "Нечётные")
                .Select(g => new { g.Key, Values = g.OrderBy(e => e), summ = g.Sum() });
            foreach (var g in arrGroup)
            {
                Console.Write($"{g.Key}: {string.Join(", ", g.Values)} Сумма: {g.summ}");
                Console.WriteLine();
            }

            Endlines();
        }

        internal static void TestLinq3()
        {
            Article("LINQ 3");

            var arr = new Worker[]
            {
                new() {Name= "Петров", Salary = 100},
                new() {Name= "Сидоров", Salary = 200},
                new() {Name= "Петров", Salary = 130}
            };
            var pairArr = arr
                .GroupBy(g => g.Name)
                .Select(e => new {e.Key, summ = arr.Where(g => g.Name == e.Key).Sum(e => e.Salary)});
            foreach (var pair in pairArr)
            {
                Console.WriteLine($"{pair.Key} - {pair.summ}");
            }

            Endlines();
        }
        class Worker
        {
            public string Name;
            public int Salary;
        }

        internal static void TestLinq4()
        {
            Article("LINQ 4");

            var arr = new int[] {1, 3, 5 , 8, 9, 1, 6, 4, 7, 8, 9, 2, 5, 2, 7, 8, 0, 3, 5, 6, 4, 2, 7, 8, 9, 4, 3, 5, 6};
            var filter = arr
                .Where(e => arr.Count(t => t == e) == 3)
                .Select(e => e)
                .Distinct();
            foreach (var e in filter) Console.WriteLine(e);

            Endlines();
        }

        internal static void TestLinq5()
        {
            Article("LINQ 5");

            var arr = new string[] {"от", "Отелло", "РоТ", "КРОТОВАЯ нора", "Лиса"};
            var sorted = arr
                .Where(e => e.Contains("от", StringComparison.OrdinalIgnoreCase))
                .Select(e => e)
                .OrderBy(e => e);
            foreach (var e in sorted) Console.WriteLine(e);

            Endlines();
        }

        internal static void TestLinq6()
        {
            Article("LINQ 6");
            Endlines();
        }
        class Stud
        {
            public string Name;
            public string SurName;
        }
    }
}
