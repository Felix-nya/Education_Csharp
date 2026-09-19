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
            object[] items = { 1, "hello", 3.14, true, null };
            foreach (object item in items) 
            {
                if (item is string) Console.WriteLine($"string: {item}");
                if (item is int) Console.WriteLine($"int: {item}");
                if (item is float) Console.WriteLine($"float: {item}");
                if (item is double) Console.WriteLine($"double: {item}");
                if (item is decimal) Console.WriteLine($"decimal: {item}");
                if (item is bool) Console.WriteLine($"bool: {item}");
                if (item is null) Console.WriteLine("null item");
            }
        }
    }
}
