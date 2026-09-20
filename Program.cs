using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
            public decimal Amount { get; }
            public string Currency { get; }
            private const int DecimalPlaces = 2;
            public static readonly Money Zero = default;
            internal Money(decimal amount, string currency)
            {
                Amount = Math.Round(amount, DecimalPlaces);
                Currency = currency;
            }
            public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount, a.Currency);
            public static Money operator -(Money a, Money b) => new(a.Amount - b.Amount, a.Currency);
            public static bool operator ==(Money a, Money b) => a.Amount == b.Amount && a.Currency == b.Currency;
            public static bool operator !=(Money a, Money b) => a.Amount != b.Amount || a.Currency != b.Currency;
            public bool Equals(Money other) => Amount == other.Amount && Currency == other.Currency;
            public override bool Equals(object? obj) => obj is Money other && Equals(other);
            public override int GetHashCode() => HashCode.Combine(Amount, Currency);
            public override string ToString() => $"{Amount.ToString("F2", CultureInfo.InvariantCulture)} {Currency}";
            public static explicit operator decimal(Money m) => m.Amount;
            public static Money Sum(params Money[] items)
            {
                if (items.Length == 0)
                    return Zero;

                Money result = items[0];
                for (int i = 1; i < items.Length; i++)
                    result += items[i];
                return result;
            }
            public static bool SameCurrency(in Money a, in Money b) => a.Currency == b.Currency;

        }
        static void Main(string[] args)
        {
            var a = new Money(10.555m, "EUR");
            var b = new Money(5m, "EUR");
            Console.WriteLine(a);
            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a == new Money(10.56m, "EUR"));
            Console.WriteLine(a != b);
            Console.WriteLine(a.ToString());
        }
    }
}
