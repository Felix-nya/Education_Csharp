using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Classes
{
    internal class OrderEventArgs(string _dishName, int _tableNumber, DateTime _orderTime) : EventArgs
    {
        internal string DishName { get; } = _dishName;
        internal int TableNumber { get; } = _tableNumber;
        internal DateTime OrderTime { get; } = _orderTime;
    }
    internal class OrderManager
    {
        internal event EventHandler<OrderEventArgs>? OrderPlaced;
        internal void PlaceOrder(string dishName, int tableNumber) => OnOrderPlaced(new(dishName, tableNumber, DateTime.Now));
        private void OnOrderPlaced(OrderEventArgs e)
        {
            OrderPlaced?.Invoke(this, e);
        }
    }
}
