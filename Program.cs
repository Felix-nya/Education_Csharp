using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        internal struct Money
        {
            private decimal Amount;
            private string Currency;
            private const int DecimalPlaces = 2;
            static readonly Money Zero;
            internal Money(decimal amount, string currency)
            {
                Amount = amount;
                Currency = currency;
            }
            public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount, a.Currency);
            static void Main(string[] args)
            {

            }
        }
    }
}
