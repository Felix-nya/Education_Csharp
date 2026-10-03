using System;
using System.Text;

namespace Test
{
    class Program
    {
        internal static void TestBank()
        {
            BankAccount MyBank = new();
            MyBank.Deposit(500);
            Console.WriteLine(MyBank.Balance);
            MyBank.Withdraw(150);
            Console.WriteLine(MyBank.Balance);
        }
        internal static void TestUser()
        {
            User _user = new();
            _user.Email = "ra@gmail.ru";
            _user.Age = 30;
            Console.WriteLine($"{_user.Email}, {_user.Age}");
            try
            {
                _user.Age = 250;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        internal static void TestRectangle()
        {
            Rectangle rect = new() { Width = 10, Height = 10 };
            Console.WriteLine(rect.Area);
        }
        internal static void TestSimpleArray()
        {
            SimpleIntArray arr = new();
            arr[1] = 5;
            for (int i = 0; i < 10; i++) Console.WriteLine(arr[i]);
            try
            {
                arr[11] = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        internal static void TestSmartTimer()
        {
            SmartTimer timer = new();
            timer.Seconds = 3665;
            Console.WriteLine($"Секунды: {timer.Seconds} Минуты: {timer.Minutes} Часы: {timer.Hours}");
        }
        internal static void TestEnums()
        {
            Random rand = new();
            Order ord1 = new() { Id = 0, ProductName = "Курица", Status = (OrderStatus)rand.Next(0, 4) };
            Order ord2 = new() { Id = 1, ProductName = "Грелка", Status = (OrderStatus)rand.Next(0, 4) };
            Order ord3 = new() { Id = 2, ProductName = "Свинина", Status = (OrderStatus)rand.Next(0, 4) };
            Order ord4 = new() { Id = 3, ProductName = "Комод", Status = (OrderStatus)rand.Next(0, 4) };
            Order ord5 = new() { Id = 4, ProductName = "Диван", Status = (OrderStatus)rand.Next(0, 4) };
            Order[] ords = { ord1, ord2, ord3, ord4, ord5 };
            foreach (Order ord in ords)
            {
                switch (ord.Status)
                {
                    case OrderStatus.New:
                        Console.WriteLine(ord.CanCancelOrder());
                        ord.DisplayOrderInfo();
                        break;
                    case OrderStatus.Processing:
                        Console.WriteLine(ord.CanCancelOrder());
                        ord.DisplayOrderInfo();
                        break;
                    case OrderStatus.Shipped:
                        ord.DisplayOrderInfo();
                        break;
                    case OrderStatus.Delivered:
                        Console.WriteLine(ord.CanCancelOrder());
                        ord.DisplayOrderInfo();
                        break;
                    case OrderStatus.Cancelled:
                        ord.DisplayOrderInfo();
                        break;
                    default:
                        break;
                }
            }

            USER user1 = new() { Name = "Oleg", Permissions = UserPermissions.ReadWriteDeleteExecute };
            USER user2 = new() { Name = "Vasa", Permissions = UserPermissions.Read };
            USER user3 = new() { Name = "Gaga", Permissions = UserPermissions.ReadWriteDelete };
            Console.WriteLine($"{user1.Name}: {user1.Permissions}, {user2.Name}: {user2.Permissions}, {user3.Name}: {user3.Permissions}");
            user2.AddPermission(UserPermissions.Write);
            user3.RemovePermission(UserPermissions.Delete);
            Console.WriteLine($"{user1.Name}: {user1.Permissions}, {user2.Name}: {user2.Permissions}, {user3.Name}: {user3.Permissions}");

            TextCommand com = TextCommand.Stop;
            ExecuteCommand(com);
            ExecuteCommand(ParseCommand("StArT"));

            Notification sms = new("Hi", NotificationType.SMS, Priority.Low);
            Notification email = Notification.CreateUrgentNotification("Important", NotificationType.Email);
            if (sms.CanSend()) sms.Send();
            email.Send();
            Notification Alll = new("spam", NotificationType.ALL, Priority.Medium);
            Notification empties = new(string.Empty, NotificationType.Push, Priority.Medium);
            Alll.Send();
            empties.Send();
        }
        internal static TextCommand ParseCommand(string input)
        {
            if (Enum.TryParse(input.Trim(), true, out TextCommand result)) return result;
            return TextCommand.Unknown;
        }
        internal static void ExecuteCommand(TextCommand com)
        {
            switch (com)
            {
                case TextCommand.Unknown:
                case TextCommand.Start:
                case TextCommand.Stop:
                case TextCommand.Pause:
                case TextCommand.Resume:
                    Console.WriteLine($"Выполняется команда {com}");
                    break;
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {

            //TestBank();
            //TestUser();
            //TestRectangle();
            //TestSimpleArray();
            //TestSmartTimer();

            TestEnums();
            Console.ReadKey();
        }
    }
}