using System;
using System.Collections.Generic;
using System.Text;
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


    }
}
