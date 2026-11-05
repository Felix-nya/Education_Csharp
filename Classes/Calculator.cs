using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Classes
{
    internal class MathOperationException : Exception
    {
        internal MathOperationException() { }
        internal MathOperationException(string message) : base(message) { }
    }

    internal class DivisionByZeroException : MathOperationException
    {
        internal DivisionByZeroException() { }
        internal DivisionByZeroException(string message) : base(message) { }
    }

    internal class NegativeRadicandException : MathOperationException
    {
        internal NegativeRadicandException() { }
        internal NegativeRadicandException(string message) : base(message) { }
    }

    internal static class Calculator
    {
        internal static double Divide(int a, int b)
        {
            if (b == 0) throw new DivisionByZeroException("Деление на ноль!");
            return a / b;
        }
        internal static double SquareRoot(int a)
        {
            if (a == -1) throw new MathOperationException("Введено число -1: i");
            if (a < 0) throw new NegativeRadicandException("Отрицательное число под корнем!");
            return Math.Sqrt(a);
        }
    }
}
