using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Classes
{
    internal class InsufficientFundsException : Exception
    {
        internal InsufficientFundsException() { }
        internal InsufficientFundsException(string message) : base(message) { }
    }
    internal class BankAccount
    {
        internal decimal Balance { get; set; }
        internal void Withdraw(decimal amount)
        {
            if (Balance < amount) throw new InsufficientFundsException($"Запрашиваемая сумма: {amount}, Доступно: {Balance}");
            if (amount < 0) throw new InsufficientFundsException($"Запрашиваемая сумма отрицательная: {amount}, Доступно: {Balance}");
            Balance -= amount;
        }
    }
}
