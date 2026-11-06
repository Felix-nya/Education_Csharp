using Test.Classes;

namespace Test
{
    internal static class Tester
    {
        internal static void TestCounter()
        {
            Counter counter = new();
            counter.CountReached += OnCountReached;
            for (int i = 0; i < 10; i++) counter.Increment();
        }
        private static void OnCountReached(object? sender, CountReachedEventArgs e)
        {
            Console.WriteLine($"«Цель достигнута! Счетчик = {e.Count}»");
        }

        internal static void TestOrderManager()
        {
            OrderManager manager = new();
            manager.OrderPlaced += OnOrderPlaced;
            manager.OrderPlaced += (sender, e) => Console.WriteLine($"Заказ принят: [{e.DishName}] для стола [{e.TableNumber}]");
            manager.PlaceOrder("Том Ям", 1);
            manager.PlaceOrder("Рамен", 2);
            manager.OrderPlaced -= OnOrderPlaced;
            manager.PlaceOrder("Шашлык", 3);
        }
        private static void OnOrderPlaced(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"Детали заказа: [{e.DishName}] | Стол: [{e.TableNumber}] | Время: [{e.OrderTime}]");
        }

        internal static void TestCacheCalculator()
        {
            Console.WriteLine(CacheCalculator<int>.Calculate(5, e => e * e));
            Console.WriteLine(CacheCalculator<int>.Calculate(5, e => e * e));
            Console.WriteLine(CacheCalculator<string>.Calculate("Hello", e => e + e));
        }

        internal static void TestAlertSystem()
        {
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
        }

        internal static void TestCalculator()
        {
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
        }

        internal static void TestShapes()
        {
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
        }
    }
}
