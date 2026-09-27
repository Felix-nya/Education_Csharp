using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    static class MathExtensions
    {
        public static int StandartDeviation(this int[] input, bool useSampleVariance = false)
        {
            double M = (double)input.Sum() / input.Length;
            if (useSampleVariance)
            {
                double sum = 0;
                foreach (int i in input) sum += Math.Pow(i - M, 2);
                return (int)Math.Sqrt(sum / (input.Length - 1));
            }
            else
            {
                double sum = 0;
                foreach (int i in input) sum += Math.Pow(i - M, 2);
                return (int)Math.Sqrt(sum / input.Length);
            }
        }
        internal static bool TryCalculateCircle(in double radius, out double Sqr, out double Len)
        {
            Sqr = Math.PI * Math.Pow(radius, 2);
            Len = 2 * Math.PI * radius;
            if (Sqr != 0 && Len != 0) return true;
            else return false;
        }
        internal static void SwapValues(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        internal static string FormatNumbers(string format = "F2", params double[] numbers)
        {
            string result = string.Empty;
            foreach (double i in numbers)
            {
                result = string.Concat(result, i.ToString(format), " | ");
            }
            return result;
        }
        internal static decimal CalculateInvestment(decimal principal, int rate, int years, bool compoundMonthly = true)
        {
            double r = rate / 100.0;
            double factor;
            if (compoundMonthly)
            {
                factor = Math.Pow(1 + r / 12, 12 * years);
            }
            else
            {
                factor = Math.Pow(1 + r, years);
            }
            return principal * (decimal)factor;
        }
    }
}
