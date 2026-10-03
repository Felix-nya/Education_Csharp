using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class BankAccount
    {
        private int _balance = 0;
        internal int Balance { get => _balance; }
        internal void Deposit(int amount) => _balance += amount;
        internal void Withdraw(int amount) => _balance -= amount;
    }
}
