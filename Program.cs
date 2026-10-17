using System;

namespace Test
{
    public delegate bool FilterCondition(int value);
    
    internal class Program
    {
        private static void Filter(ref int[] values, FilterCondition filter)
        {
            int[] result = new int[values.Length];
            int k = 0;
            foreach (int i in values)
            {
                if (filter(i))
                {
                    result[k] = i;
                    k++;
                }
            }
            values = result;
        }
        private static void TestFilter()
        {
            
            int[] values1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            int[] values2 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            foreach (int i in values1) Console.Write($"{i} ");
            Console.WriteLine();

            FilterCondition oddFilter = n => n % 2 == 0;
            FilterCondition simpleFilter = IsSimple;
            Filter(ref values1, oddFilter);
            Filter(ref values2, simpleFilter);

            foreach (int i in values1) if (i != 0) Console.Write($"{i} ");
            Console.WriteLine();
            foreach (int i in values2) if (i != 0) Console.Write($"{i} ");
            Console.WriteLine();
        }
        private static bool IsSimple(int value)
        {
            for (int i = 2; i <= (int)double.Sqrt(value); i++)
            {
                if (value % i == 0) return false;
            }
            return true;
        }
        private static void Main(string[] args)
        {
            TestFilter();

            Console.ReadKey();
        }
    }
}