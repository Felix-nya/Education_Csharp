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
        static void Main(string[] args)
        {
            int valu = 10;
            Object valu2 = valu;
            Console.WriteLine($"int: {valu}" + $"\t double: {valu2}");
            int valu3 = (int)valu2;
            Console.WriteLine($"int2: {valu3}");
            try
            {
                int value = 10;
                Object value2 = value;
                Console.WriteLine($"int: {value}" + $"\t double: {value2}");
                double value3 = (double)value2;
                Console.WriteLine($"double2: {value3}");
            } catch
            {
                Console.WriteLine("error");
            }
        }
    }
}
